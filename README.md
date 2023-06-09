# FTP downloader

*Создан: Март - Июнь 2023*

#### Приложение представляет собой простой FTP клиент для скачивания файлов. Возможно скачивание до 10 файлов одновременно. Записи о удачных и неудачных загрузках записываются в журнал. Имеется панель уведомлений, сообщающая о результатах операций.

<br/>

Приложение написано на платформе WPF по шаблону MVVM. Используемая база данных - SQLite. Для доступа к БД используется EF Core и подход Code first. Для работы по протоколу FTP используется библиотека FluentFtp. Для сборки и запуска приложения используется Generic Host. Архитектура приложения реализует инверсию зависимостей. Приложение содержит множество асинхронных операций. Пользовательский интерфейс хорошо проработан.

<br/>

<table>
  <td><img src="https://github.com/NB034/FTP-downloader/assets/104451273/61de1e68-e296-4073-9e64-91aa7b419826" width="411" height="303"/></td>
  <td><img src="https://github.com/NB034/FTP-downloader/assets/104451273/b16b1aef-0a14-43b0-9152-0336eb53ba7d" width="411" height="303"/></td>
<table>
	
<br/>
	
---
	
<br/>
	
#### Инструкция:
1. Ввести имя пользователя и пароль, либо отключить строку, сняв галочку напротив.
2. Ввести адрес хоста и путь к ресурсу. Затем нажать кнопку "Check". Приложение попробует связаться с хостом, чтобы запросить информацию о файле.
3. Нажать на кнопку ". . .", чтобы выбрать путь для скачивания файла.
4. При необходимости изменить имя, с которым файл будет сохранён.
5. При желании добавить до 5 тегов, по которым впоследствии можно будет найти запись о загрузке в журнале.
6. Нажать на кнопку "Initialize download" для начала скачивания. В процессе загрузку можно остановить, возобновить и отменить.
  
<br/>
	
---
	
<br/>
	
*На видео демонстрируется использоапние программы для работы с FTP сервером FileZilla, запущенном на том же компьютере.*  
	
<br/>

https://github.com/NB034/FTP-downloader/assets/104451273/b9a6bf93-f6a1-4287-860d-b8c6e47cf0e2

<!-- Original size: 1233x910 -->
<!-- Compressed size (3/4): 925x683 -->
<!-- Compressed size (1/2): 617x455 -->
<!-- Compressed size (2/5): 493x364 -->
<!-- Compressed size (1/3): 411x303 -->
<!-- Compressed size (1/4): 308x228 -->
	
