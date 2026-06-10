# KPosSystem

## Project Overview

KPosSystem is a WPF desktop application.

This system uses Azure SQL Database and consists of the following projects:

* wpf

  * UI layer
  * Windows
  * UserControls
  * ViewModels

* shared

  * Shared business logic
  * Common utilities
  * DTOs

* azureDatabase

  * Azure SQL Database access
  * Repository classes
  * SQL execution

---

## Language

* 日本語で回答する
* ソースコードコメントは日本語で記載する

---

## Technology

* C#
* WPF
* .NET 8
* Azure SQL Database

---

## Build

変更後は必ず以下を実行してビルド成功を確認する。

```bash
dotnet build KPosSystem.sln
```

ビルドエラーが発生した場合は修正を継続し、ビルド成功まで対応する。

---

## Architecture

### wpf

UI層

責務:

* Window
* UserControl
* ViewModel
* UIイベント処理

### shared

共通処理層

責務:

* 共通ロジック
* DTO
* Utility
* 定数定義

### azureDatabase

データアクセス層

責務:

* Azure SQL Database接続
* Repository
* SQL実行

---

## Mandatory Rules

* 日本語で回答する
* 変更前に実装方針を説明する
* 変更対象ファイルを明示する
* ビルド成功を確認する
* 既存アーキテクチャを維持する
* 不要なリファクタリングを行わない
* 影響範囲を説明する

---

## Restrictions

以下は事前承認なしに実施しない。

* NuGetパッケージ追加
* テーブル定義変更
* 接続文字列変更
* フォルダ構成変更
* プロジェクト追加
* ファイル削除
* 大規模リファクタリング

---

## Database Rules

データベース関連の変更を行う場合:

1. 対象テーブルを特定する
2. 対象SQLを特定する
3. 影響範囲を説明する
4. 実装方針を説明する

接続文字列は変更しない。

---

## WPF Rules

* 既存画面と同じ実装パターンを優先する
* 既存スタイルを再利用する
* 新しいUIフレームワークは導入しない
* 既存MVVM構造を維持する

---

## Async Rules

* DBアクセスは async/await を優先する
* UIスレッドをブロックしない

---

## Debug Rules

不具合対応時は以下を実施する。

1. 原因調査
2. 原因説明
3. 修正方針説明
4. 最小変更で修正
5. ビルド確認

推測による修正は避ける。

---

## Output Format

作業結果は以下の形式で報告する。

### 修正概要

変更内容の要約

### 変更ファイル

変更したファイル一覧

### ビルド結果

成功 / 失敗

### 影響範囲

影響を受ける機能

### 残課題

未対応事項があれば記載


## 開発フロー

### ツール役割分担
- Claude Desktop: 実装方針の確認・整理・Claude Code用プロンプトの生成
- Claude Code: 実際のファイル操作・実装・ビルド確認

### Claude Desktopでの作業手順
1. 実装したい内容をClaude Desktopに伝える
2. 実装方針・対象ファイルを確認する
3. Claude DesktopがClaude Code用プロンプトを生成する
4. プロンプトをClaude Codeに貼り付けて実装させる

### Claude Code用プロンプト生成ルール
Claude Desktopは以下を含むプロンプトを生成する。
- 実装方針
- 変更対象ファイル
- 具体的な実装内容
- ビルド確認の指示（dotnet build KPosSystem.sln）

### Claude Code起動方法
C:\azure\KPosSystem\start-claude.bat を実行する。
