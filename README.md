## ライフゲーム with XAML.
以前 `Xamarin` のコードだけで作ったライフゲームのリメイク版です。<br/>
今回は `XAML` と `Prism.Forms` を使っています

### 各種バージョン
ライブラリ | バージョン 
--- | --- |
Xamarin.Forms | 2.3.3.180 
Prism.Core | 6.2.0 
Prism.Forms | 6.2.0 
Prism.Unity.Forms | 6.2.0 

### 前回との変更点
#### ViewにXAMLを使った
`Grid` や `Button` のレイアウトの設定は、`XAML` でやりました。<br>
コードでやると動かすまでイマイチコンポーネントを配置しているのをイメージしにくかったですけど、<br>
`XAML` を使うことで動かすまえでもある程度レイアウトをイメージしやすかったです。<br>
ただ、`Grid` 上に置くたくさんの `Cell` 達を `XAML` で実現しようとすると力技になって色々大変だったので、
そこだけはコード上で行いました。<br>

#### PrismでMVVM
`Button` や `Cell` のイベントハンドラの作成や、ライフゲームの処理の部分を `ViewModel` に記述しました。<br>
イベントハンドラは、`DelegateCommand` を用いることで、クリックできる/できない状態を制御し、より操作しやすいように変更できました。<br>

### 得られたこと
#### XAMLとコードビハインドの共存の重要性
どちらにも得手不得手があるので、うまく両方を使うとハッピーになると思います。<br>
基本的に `View` は `XAML` で作成するけど、繰り返しが必要なものや設定にちょっと処理が必要なものについては、
コードビハインド側で作成・配置を行った方がよいなと思いました。

#### DelegateCommandの使い方
`DelegateCommand` は、型パラメータのあり/なしでちょっとだけ書き方が変わり、型パラメータありの場合は `CommandParameter` での値の受け渡しが可能でした。<br>
ただ、値の受け渡し時に指定する型は、`Nullable` （Null許容）じゃないといけなくて、<br>
`int` を指定したら起動時にエラーになり、`int?` に変更すると大丈夫でした。

### 参考にしたもの
 | ページ | 内容|
 | --- | --- |
 | [chomado/Xamalist](https://github.com/chomado/Xamalist) | Prismの実装（特にDelegateCommand） |
 | [nuits.jp blog](http://www.nuits.jp/) | Prism関連のトラブルシューティング |
 | [かずきのBlog@hatena](http://blog.okazuki.jp/) | XAMLの実装 |
