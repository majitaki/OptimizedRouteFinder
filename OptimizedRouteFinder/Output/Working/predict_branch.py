import pandas as pd
import csv
from datetime import datetime as dt
import os
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '2'
import numpy as np
import copy
import matplotlib.pyplot as plt
import glob
import winsound
import time
import sys

from keras.models import Sequential
from keras.layers.core import Dense, Dropout, Activation
from keras.layers import Convolution1D, MaxPool1D
from keras.callbacks import TensorBoard
from keras.optimizers import RMSprop
from keras.optimizers import Adam
from keras.callbacks import EarlyStopping
from keras.layers.normalization import  BatchNormalization
from keras.callbacks import ModelCheckpoint
from keras.utils import np_utils
from keras.models import model_from_json

#--設定--
comment = 'ここにコメントを記載してください'

#学習データの場所
output_pass = './'
learn_data_pass = './data/random_honsyu.csv'
predict_data_pass = './sample.csv'
model_pass = './model/'
#正解データの場所
Target_data = 1

#結果出力のルート場所
result_pass = './results/'

#学習の設定
#テストデータの割合(全体データのうち)
test_ratio = 0.1
#検証データの割合(学習データのうち)
val_ratio = 0.1
#パラメータ設定
BATCHES = 100 #10
EPOCHS = 1000 #1000
REPEAT = 1 #20

#重要度解析の設定
rm_groups = [
#            [''],
            ['A', 'B', 'D', 'F', 'I', ],#No1_貨物unique詳細
#            ['C', 'E', 'G', 'N', 'O', ],#No2_貨物unique粗
#            ['e0', 'e1', 'e2', 'e3', 'l0', 'l1', 'l2', 'l3', 'm0', 'm1', 'm2', 'm3', 'n0', 'n1', 'n2', 'n3', 'q0', 'q1', 'q2', 'q3', ],#No3_陸路unique詳細
#            ['i0', 'i1', 'i2', 'i3', 'j0', 'j1', 'j2', 'j3', 'k0', 'k1', 'k2', 'k3', 'o0', 'o1', 'o2', 'o3', 'p0', 'p1', 'p2', 'p3', ],#No4_陸路unique粗
#            ['A', 'B', 'D', 'F', 'I', 'e0', 'e1', 'e2', 'e3', 'l0', 'l1', 'l2', 'l3', 'm0', 'm1', 'm2', 'm3', 'n0', 'n1', 'n2', 'n3', 'q0', 'q1', 'q2', 'q3', ],#No5_貨物unique詳細・経路unique詳細
#            ['A', 'B', 'D', 'F', 'I', 'i0', 'i1', 'i2', 'i3', 'j0', 'j1', 'j2', 'j3', 'k0', 'k1', 'k2', 'k3', 'o0', 'o1', 'o2', 'o3', 'p0', 'p1', 'p2', 'p3', ],#No6_貨物unique詳細・経路unique粗
#            ['C', 'E', 'G', 'N', 'O', 'e0', 'e1', 'e2', 'e3', 'l0', 'l1', 'l2', 'l3', 'm0', 'm1', 'm2', 'm3', 'n0', 'n1', 'n2', 'n3', 'q0', 'q1', 'q2', 'q3', ],#No7_貨物unique粗・経路unique詳細
#            ['C', 'E', 'G', 'N', 'O', 'i0', 'i1', 'i2', 'i3', 'j0', 'j1', 'j2', 'j3', 'k0', 'k1', 'k2', 'k3', 'o0', 'o1', 'o2', 'o3', 'p0', 'p1', 'p2', 'p3', ],#No8_貨物unique粗・経路unique粗
#            ['A', 'B', 'D', 'F', 'I', 'N', 'O', 'b0', 'b1', 'b2', 'b3', 'd0', 'd1', 'd2', 'd3', ],#No9_貨物unique詳細・地理情報なし
#            ['A', 'C', 'E', 'G', 'I', 'N', 'O', 'b0', 'b1', 'b2', 'b3', 'd0', 'd1', 'd2', 'd3', ],#No10_貨物unique粗・地理情報なし
#            ['A', 'I', 'N', 'O', 'b0', 'b1', 'b2', 'b3', 'd0', 'd1', 'd2', 'd3', 'e0', 'e1', 'e2', 'e3', 'l0', 'l1', 'l2', 'l3', 'm0', 'm1', 'm2', 'm3', 'n0', 'n1', 'n2', 'n3', 'q0', 'q1', 'q2', 'q3', ],#No11_陸路unique詳細・地理情報なし
#            ['A', 'I', 'N', 'O', 'b0', 'b1', 'b2', 'b3', 'd0', 'd1', 'd2', 'd3', 'i0', 'i1', 'i2', 'i3', 'j0', 'j1', 'j2', 'j3', 'k0', 'k1', 'k2', 'k3', 'o0', 'o1', 'o2', 'o3', 'p0', 'p1', 'p2', 'p3', ],#No12_陸路unique粗・地理情報なし
#            ['C97']
            ]

#--関数--

#csv読み込み
def jdata_read(pass_name):
    try:
        df = pd.read_csv(pass_name, encoding="SHIFT-JIS")
    except FileNotFoundError as e:
        print(e)
    except csv.Error as e:
        print(e)
    
    df = df.fillna(0)
    return df

def data_read(pass_name, needless_columns):
    try:
        df = pd.read_csv(pass_name)
    except FileNotFoundError as e:
        print(e)
    except csv.Error as e:
        print(e)
        
    for column in needless_columns:
        df = df.drop(column, axis=1)    
    df = df.fillna(0)
    return df


def pred_data_read(pass_name,learn_data, needless_columns):
    try:
        df = pd.read_csv(pass_name)
    except FileNotFoundError as e:
        print(e)
    except csv.Error as e:
        print(e)
        
    for column in needless_columns:
        df = df.drop(column, axis=1)    
    df = df.fillna(0)
    
    df.columns = learn_data.columns
    return df

#学習，テストデータの分割
def data_split(data, test_ratio):
    raw_training_data = pd.DataFrame()
    raw_test_data = pd.DataFrame()
    
    training_num = round(data.shape[0] * (1 - test_ratio))
    test_num = data.shape[0] - training_num
    raw_training_data = raw_training_data.append(data[:training_num], ignore_index=True)
    raw_test_data = raw_test_data.append(data[training_num:training_num + test_num], ignore_index=True)
    return raw_training_data, raw_test_data

#列の除去
def data_remove(raw_data, rm_single):
    if rm_single == ['']:
        data = raw_data
    else:
        data = raw_data.drop(rm_single, axis=1)

    return data

#データ整形
def data_shape(training_data):  
    columns_length = len(training_data.columns)
    for i in range(columns_length):
        if (i!=0):
            t_max = training_data.iloc[:, i].max()
            t_min = training_data.iloc[:, i].min()
            training_data.iloc[:, i] = (training_data.iloc[:, i] - t_min) / (t_max - t_min)
        else:
            training_data.iloc[:, i] = (training_data.iloc[:, i])
    return training_data

def np_convert(data):
    data=data.as_matrix()
    y_data = np.zeros((data.shape[0],1));
    for i in range(data.shape[0]):
      y_data[i,0]  = data[i,0]
    y_data = np_utils.to_categorical(y_data)
    data=np.delete(data, range(0, 1, 1),1);
    x_data = data;
    return x_data, y_data

#モデル定義
def build_model(input_num):
    model = Sequential()
    model.add(Dense(input_num, input_shape=(input_num,)))
    model.add(BatchNormalization())
    model.add(Activation('linear'))
    model.add(Dropout(0.2))
    model.add(Dense(input_num))
    model.add(BatchNormalization())
    model.add(Activation('relu'))
    model.add(Dropout(0.2))
    model.add(Dense(input_num))
    model.add(BatchNormalization())
    model.add(Activation('relu'))
    model.add(Dropout(0.2))
    model.add(Dense(input_num))
    model.add(BatchNormalization())
    model.add(Activation('relu'))
    model.add(Dropout(0.2))
    model.add(Dense(input_num))
    model.add(BatchNormalization())
    model.add(Activation('relu'))
    model.add(Dropout(0.2))
    model.add(Dense(input_num))
    model.add(BatchNormalization())
    model.add(Activation('relu'))
    model.add(Dropout(0.2))
    model.add(Dense(64))
    model.add(BatchNormalization())
    model.add(Activation('relu'))
    model.add(Dropout(0.2))
    model.add(Dense(32))
    model.add(BatchNormalization())
    model.add(Activation('relu'))
    model.add(Dropout(0.2))
    model.add(Dense(4))
    model.add(BatchNormalization())
    model.add(Activation('linear'))

    #モデル評価
    model.compile(loss="mean_squared_error", optimizer=Adam(),metrics=['accuracy'])
    return model

def training_model(model, x_training_data, y_training_data, batchs,epochs, path, val_ratio):
    
    fpath = path + "model.best.h5"
    cp_cb = ModelCheckpoint(filepath=fpath, monitor='val_acc', verbose=1, save_best_only=True, mode='auto')
    #es_cb = EarlyStopping(monitor='val_acc', patience=10, verbose=1, mode='auto')
    #es_cb = EarlyStopping(monitor='val_loss', patience=10, verbose=1, mode='auto')
    hist = model.fit(x_training_data, y_training_data, batch_size=batchs, verbose=1, epochs=epochs, validation_split=val_ratio, callbacks=[cp_cb])#,TensorBoard_cb])
    #hist = model.fit(train, answer, batch_size=batchs, verbose=1, epochs=epochs, validation_split=0.1, callbacks=[cp_cb, es_cb])
    #loss_and_metrics = model.evaluate(train, answer)
    return hist

#--メイン--

#csv読み込み
needless_columns = ['random', 'number']
args = sys.argv
#learn_data = data_read(learn_data_pass, needless_columns)
learn_data = data_read(args[1], needless_columns)
predict_data = pred_data_read(predict_data_pass, learn_data, needless_columns)
all_data = pd.concat([learn_data, predict_data])
#raw_training_data = data_split(all_data, test_ratio)[0]
#raw_test_data = data_split(all_data, test_ratio)[1]

ds_rmgroup_columns = all_data.columns.delete(loc=0)
df_rmgroup = pd.DataFrame()
index0 = 0
for rm_group in rm_groups:
    index0 += 1
#        rm_name = '_rm'
    rm_name = '_'
    for r in rm_group:
        r = r.replace('cargo', '')
        #rm_name += '_'+ r
        rm_name += r
#        rm_pass = '{0:04d}'.format(index0) + rm_name
    rm_pass = '{0:04d}'.format(index0)
    
    #インデックス番号と除去列の対応表の出力
    ds_rmgroup = pd.Series(index  = ds_rmgroup_columns)
    for rmc in ds_rmgroup_columns:
        for rm in rm_group:
            if rmc == rm:
                ds_rmgroup[rmc] = 1
    
    df_rmgroup = pd.concat([df_rmgroup, pd.DataFrame(ds_rmgroup, columns=[index0])], axis=1)
    #df_rmgroup.to_csv('rmgroup.csv')
    
    
    pd_test_result = pd.DataFrame(columns = ['test_loss', 'test_acc'])
    
    for rep in range(0, REPEAT):
        
        #列の除去
        #training_data = data_remove(raw_training_data, rm_group)
        #test_data = data_remove(raw_test_data, rm_group)
        all_remed_data = data_remove(all_data, rm_group)
        #データ整形
        #training_shaped_data, test_shaped_data = data_shape(training_data, test_data)
        all_shaped_data = data_shape(all_remed_data)
        #npdata変換
        x_all_data = np_convert(all_shaped_data)[0]
        y_all_data = np_convert(all_shaped_data)[1]
        #x_test_data = np_convert(test_shaped_data)[0]
        #y_test_data = np_convert(test_shaped_data)[1]
        
        #model読み込み
        model = model_from_json(open(model_pass + 'model.json').read())
        model.load_weights(model_pass + 'model_wight.h5')
        model.summary();
        model.compile(loss="mean_squared_error", optimizer=Adam(),metrics=['accuracy'])
        
        #評価
        test_loss, test_acc = model.evaluate(x_all_data, y_all_data, verbose=0)
        series = pd.Series([test_loss, test_acc], index= ['test_loss', 'test_acc'])
        pd_test_result = pd_test_result.append(series, ignore_index = True)
        
        predict_test = model.predict(x_all_data, batch_size=BATCHES,verbose=0)
        predicted_list = []
        value_list =[]
        df_result = pd.DataFrame()
        
        df_predict_test = pd.DataFrame(predict_test)
        for i in range(df_predict_test.shape[0]):
            p_max = np.array(df_predict_test.iloc[[i]]).argmax()
            predicted_list.append(p_max)
            value_list.append(df_predict_test.iloc[[i],p_max][i])
        
        df_result['estimate'] = pd.Series(predicted_list)
        df_result['value'] = pd.Series(value_list)
        df_result.iloc[len(learn_data):,:].to_csv(output_pass + 'result.csv', encoding="SHIFT-JIS")
        df_result.iloc[len(learn_data):,:].to_csv(output_pass + 'flag', encoding="SHIFT-JIS")
        #output_data = copy.deepcopy(raw_test_data)
        #output_data['predict_result'] = est_list
        #output_data.to_csv(output_pass + 'result_test_all.csv', encoding="SHIFT-JIS")
        #winsound.Beep(523, 1000)