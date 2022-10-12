# FishNetSceneManagerSample

## 前提条件
※クローンした後にこちらのライブラリを手動で入れてください

- FishNetworking.2.3.14
- ParrelSync-1.5.1

## FishNetSceneManagerを利用して全Clientが指定のシーンを遷移させる方法

```
//遷移先のシーン情報をまとめたクラスを生成
var sld = new SceneLoadData($"遷移先のシーン名") { 
//遷移したら他のシーンは削除する設定の場合
ReplaceScenes =  ReplaceOption.All

};

//LoadGlobalScenesの引数に先ほどのデータを入れる
NetworkManager.SceneManager.LoadGlobalScenes(sld);
```
