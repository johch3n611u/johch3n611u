// javascript 
// 為直譯式 Interpret ->網頁載入時就會從第一行執行至最後一行。https://zh.wikipedia.org/wiki/%E7%9B%B4%E8%AD%AF%E8%AA%9E%E8%A8%80
// 為事件驅動 Event-driven ->當事件監聽器監聽到訊號時執行該監聽器呼叫之功能。https://zh.wikipedia.org/wiki/%E4%BA%8B%E4%BB%B6%E9%A9%85%E5%8B%95%E7%A8%8B%E5%BC%8F%E8%A8%AD%E8%A8%88
// 為物件導向式 Object-oriented ->類別、物件、狀態、繼承...。https://zh.wikipedia.org/wiki/%E9%9D%A2%E5%90%91%E5%AF%B9%E8%B1%A1%E7%A8%8B%E5%BA%8F%E8%AE%BE%E8%AE%A1
// 為手稿語言 Scripting ->藉由指令碼將常用的操作組成序列。https://zh.wikipedia.org/wiki/%E8%84%9A%E6%9C%AC%E8%AF%AD%E8%A8%80


// ****************************************************
var audio = document.getElementById("audio");
var play = document.getElementById("play");
//var pause = document.getElementById("pause");
var stop = document.getElementById("stop");
var info = document.getElementById("info");
var music = document.getElementById("music");
var song = document.getElementById("song");
var prevtime = document.getElementById("prevtime");
var nexttime = document.getElementById("nexttime");
var prevsong = document.getElementById("prevsong");
var nextsong = document.getElementById("nextsong");

var vol = document.getElementById("vol");
var volM = document.getElementById("volM");
var volP = document.getElementById("volP");
var volValue = document.getElementById("volValue");
var muted = document.getElementById("muted");
var loop = document.getElementById("loop");
var random = document.getElementById("random");
var allloop = document.getElementById("allloop");

var info2 = document.getElementById("info2");
var duration = document.getElementById("duration");
var settime = document.getElementById("settime");

var Sbook = document.getElementById("Sbook");
var Tbook = document.getElementById("Tbook");
var btnUpdateMusic = document.getElementById("btnUpdateMusic");
var songbook = document.getElementById("songbook");
var book = document.getElementById("book");
// 此處:抓取html tag之屬性並塞入變數盒子中做以下功能上的利用。




// ****************************************************
play.addEventListener("click", PlayMusic);
//pause.addEventListener("click", PauseMusic);
stop.addEventListener("click", StopMusic);
music.addEventListener("change", function() { ChangeMusic(music.selectedIndex); });
prevtime.addEventListener("click", function() { TimeChange(false); });
nexttime.addEventListener("click", function() { TimeChange(true); });
prevsong.addEventListener("click", function() { SongChange("p"); });
nextsong.addEventListener("click", function() { SongChange("n"); });
vol.addEventListener(BrowserTest(), function() { VolumeChange("c"); });
volM.addEventListener("click", function() { VolumeChange("m"); });
volP.addEventListener("click", function() { VolumeChange("p"); });
muted.addEventListener("click", SetMuted);
loop.addEventListener("click", loopSong);
random.addEventListener("click", randomSong);
allloop.addEventListener("click", allloopSong);
settime.addEventListener("click", function() { setTimeUseBar(event); });
// 此處:用於監聽按鈕觸發事件與瀏覽器。
Sbook.addEventListener("dragstart", function() { drag(event); });
Tbook.addEventListener("dragover", function() { allowDrop(event); });
Tbook.addEventListener("drop", function() { drop(event); });

Tbook.addEventListener("dragstart", function() { drag(event); });
Sbook.addEventListener("dragover", function() { allowDrop(event); });
Sbook.addEventListener("drop", function() { drop(event); });
// 此處:用於監聽拖移開始結束放下事件。
btnUpdateMusic.addEventListener("click", UpdateMusic);
songbook.addEventListener("click", function() { displayBook(book.title) });
// 此處:之上監聽web window瀏覽器頁面當前事件並觸發功能。



// ****************************************************
VolumeChange("c"); //設定初始音量
getDefaultSong(); //設定初始歌單
// 此處:呼叫此兩個功能，並傳入相對應的值。






// ****************************************************
//瀏覽器偵測
// 此處:藉由偵測瀏覽器轉換相對應標籤，使其轉換瀏覽器時能正常操作。
function BrowserTest() {
    if (navigator.userAgent.search("Chrome") != -1)
        return "input";
    else if (navigator.userAgent.search("Opera") != -1)
        return "input";
    else if (navigator.userAgent.search("Firefox") != -1)
        return "input";
    else
        return "change";

}




// ****************************************************
// 此處:用於轉換標籤屬性，藉此顯示隱藏歌本歌能。
function displayBook(displayFlag) {
    if (displayFlag == "hide") {
        book.title = "show";

    } else {
        book.title = "hide";

    }
    book.className = book.title;
}






// ****************************************************
// 此處:用於歌本清單選定後，"更新歌單"按鈕功能。
//更新歌單
function UpdateMusic() {
    //清掉所有歌
    // 邏輯:倒著清避免位址&內容搞混。
    for (i = music.children.length - 1; i >= 0; i--) {
        music.removeChild(music.children[i]);
    }

    // 此處:將選擇的歌曲轉移至撥放清單中。
    for (i = 0; i < Tbook.children.length; i++) {
        var option = document.createElement("option");
        var Snode = Tbook.children[i];
        option.value = Snode.title;
        option.innerText = Snode.innerText;
        music.appendChild(option);

    }
    // 呼叫:歌曲轉換功能並傳值a
    // (這邊是為了不重複相同功能程式碼，所以更改了部分功能與物件並集中於(SongChang function)中呼叫，做了一個增內聚減耦合縮減程式碼動作。
    // 個人認為老師少了一點小細節，縮排與更改功能後，並沒有重新利用匈牙利命名法，更改功能與物件id名稱，導致id語意不相對應情況。
    SongChange("a");
}





// ****************************************************
// 此處:用於"拖移相關"事件之功能。
// 此處:preventDefault()為方法，手稿語言對物件做直接指令。https://developer.mozilla.org/zh-TW/docs/Web/API/Event/preventDefault
// preventDefault() 方法:如果事件可以被取消，就取消事件（即取消事件的預設行為）。但不會影響事件的傳遞，事件仍會繼續傳遞。
function drop(evt) {
    evt.preventDefault();
    var data = evt.dataTransfer.getData("text");
    evt.target.appendChild(document.getElementById(data));
}
// 此處:用於拖移相關事件之功能。
function allowDrop(evt) {
    evt.preventDefault();
}

// 此處:用於拖移相關事件之功能。https://developer.mozilla.org/zh-TW/docs/Web/API/DataTransfer
// dataTransfer 物件:用以存放拖移操作過程中的資料。
function drag(evt) {
    evt.dataTransfer.setData("text", evt.target.id);
    //alert(evt.target.id);
}


//抓預設歌曲
// 此處:當歌單有拖移增加物件時更新歌單顯示內容，並增加其拖移屬性與ID。
// http://www.w3school.com.cn/html/html5_draganddrop.asp
// http://www.w3school.com.cn/html5/html_5_draganddrop.asp
// createElement 事件:創建元素。https://developer.mozilla.org/zh-TW/docs/Web/API/Document/createElement
// appenChild() 方法:向節點添加最後一個子節點。https://www.google.com/search?q=js+appendChild&rlz=1C1GCEU_zh-TWTW835TW836&oq=js+appendChild&aqs=chrome..69i57j0l5.1819j0j7&sourceid=chrome&ie=UTF-8
// draggable 標籤:HTML5新標籤，定義物件是否可被拖移。http://www.w3school.com.cn/tags/att_global_draggable.asp
function getDefaultSong() {
    for (i = 0; i < Sbook.children.length; i++) {
        var option = document.createElement("option");
        var Snode = Sbook.children[i];
        option.value = Snode.title;
        option.innerText = Snode.innerText;
        music.appendChild(option);

        Snode.draggable = true;
        Snode.id = "song" + (i + 1);
    }

    SongChange("a");
}


// ****************************************************
// 此處:用於抓取音效檔按當前撥放時間資料，並轉換為CSS樣式顯示物件，並讓此物件得以操控。
function setTimeUseBar(evt) {
    //console.log(ControlPanel.style.width);
    audio.currentTime = audio.duration * (evt.offsetX / parseInt(ControlPanel.style.width));
    //console.log(ControlPanel.style.width);
}




// ****************************************************
// 此處:用於顯示當前撥放之音效檔的資訊。
// duration 屬性:返回當前媒體長度。http://www.w3school.com.cn/tags/av_prop_duration.asp
// currentTime 屬性:設置當前媒體長度。http://www.w3school.com.cn/tags/av_prop_currenttime.asp
function getDuration() {
    var durationTime = getTimeFormat(audio.duration);
    var currentTime = getTimeFormat(audio.currentTime);
    duration.innerText = currentTime + " / " + durationTime;
    // 此處:用於歌曲進度檻功能，將數據轉為CSS。
    // setTimeout() 方法:用於指定數秒後調用函數或計算表達式。http://www.w3school.com.cn/htmldom/met_win_settimeout.asp
    progress.style.width = Math.floor(audio.currentTime / audio.duration * 100) + "%";
    console.log(progress.style.width);
    setTimeout(getDuration, 1000);


    // 此處:利用音效當前時間與整體時間做比對，在相等時相當於撥放結束，此時判斷info2裡的值，確定須進行哪種撥放模式。
    if (durationTime == currentTime) {
        if (info2.innerText == "單曲循環") {
            SongChange("s");
        } else if (info2.innerText == "隨機播放") {
            SongChange("r");
        } else if (music.selectedIndex == music.options.length - 1) {
            if (info2.innerText == "全曲循環") {
                SongChange("a");

            } else {
                StopMusic();
            }
        } else {
            SongChange("c");

        }

    }

}

// 此處:解決顯示時間資訊時不符合需求之顯示。
// floor 方法:對一物件進行四捨五入取整數。http://www.w3school.com.cn/js/jsref_floor.asp
// Math 物件:算數物件??。http://www.w3school.com.cn/js/jsref_obj_math.asp
function getTimeFormat(TimeSec) {
    var min = Math.floor(TimeSec / 60);
    var sec = Math.floor(TimeSec) - min * 60;
    min = min < 10 ? "0" + min : min;
    sec = sec < 10 ? "0" + sec : sec;
    return min + ":" + sec;
}


// ****************************************************
// 此處:利用監聽器呼叫時傳遞值至上方資訊欄功能內之時間物件進行判斷並傳值在更下方執行確切功能。
//全曲循環
// 此處:當info2裡面的值為全曲循環時，指定info2為空值，否則傳送全曲循環字串至info2中。
function allloopSong() {
    if (info2.innerText == "全曲循環") {
        info2.innerText = "";
    } else {
        info2.innerText = "全曲循環";
    }
}

//隨機播放
// 此處:當info2裡面的值為隨機播放時，指定info2為空值，否則傳送隨機播放字串至info2中。
function randomSong() {
    if (info2.innerText == "隨機播放") {
        info2.innerText = "";
    } else {
        info2.innerText = "隨機播放";
    }
}

//單曲循環
// 此處:當info2裡面的值為單曲循環時，指定info2為空值，否則傳送單曲循環字串至info2中。
function loopSong() {
    if (info2.innerText == "單曲循環") {
        info2.innerText = "";
    } else {
        info2.innerText = "單曲循環";
    }
}

// ****************************************************
// 此處:以下為聲音設置相關功能。
// 此處為靜音功能。
function SetMuted() {
    if (muted.innerText == "V") {
        audio.muted = true;
        muted.innerText = "U";
    } else {
        audio.muted = false;
        muted.innerText = "V";
    }
}
// 此處為調整音量功能。
function VolumeChange(sound) {
    switch (sound) {
        case "m":
            vol.value--;
            break;
        case "p":
            vol.value++;
            break;

    }
    volValue.value = vol.value;

    audio.volume = volValue.value / 100;
}


// ****************************************************
// 此處:因為曾經縮減程式碼，降耦合提內聚但是沒有依語意改變物件與功能id，所以有點混亂。
// 邏輯:根據strflag所傳進來的值，選擇相對應函式執行，避免重寫程式碼。
function SongChange(strFlag) {
    var index = music.selectedIndex;
    // selectedIndex 屬性:可設置或返回下拉列表中被選項的索引號。http://www.w3school.com.cn/htmldom/prop_select_selectedindex.asp
    var length = music.options.length;
    // options 集合:返回所有option可返回包含<select> 標籤中所有<option> 的一個數組。
    // http://www.w3school.com.cn/htmldom/coll_select_options.asp
    // https://www.w3schools.com/jsref/dom_obj_htmlcollection.asp
    switch (strFlag) {
        case "n":
            if (index < length - 1)
                index++;
            else
                index = 0;
            break;
            // 此處:為nextsong上一首之功能。

        case "p":
            if (index == 0)
                index = length - 1;
            else
                index--;
            break;
            // 此處:為prevsong下一首之功能。

        case "s":
            audio.currentTime = 0;
            break;
            // 此處:為單曲循環之功能。
        case "r":
            index = Math.floor(Math.random() * length);
            break;
            // 此處:為隨機撥放之功能。
        case "a":
            index = 0;
            break;
            // 此處:為全曲循環之功能。
        default:
            index++;
            // 此處:表示無任何指定功能時，自動切換下一首。
            break;

    }

    //song.src = music.options[index].value;
    //music.options[index].selected = true;

    // 呼叫切換音樂功能並將值傳入。
    ChangeMusic(index);
}

// ****************************************************
// 此處:用於快倒轉按鈕功能。
function TimeChange(boolFlag) {
    if (boolFlag)
        audio.currentTime += 10;
    else
        audio.currentTime -= 10;
}

// ****************************************************
// 此處:用於播放與暫停按鈕功能。
// 邏輯:播放時按一下轉為暫停，暫停時按一下轉為播放。
// play() 方法:開始播放當前媒體檔案。

function PlayMusic() {
    audio.play();
    getDuration();
    // 呼叫:當前播放之音效檔的資訊。
    ShowStatus(1);
    if (play.innerText == "4")
        play.innerText = ";";
    else {
        play.innerText = "4";
        PauseMusic();
    }
}

// ****************************************************
// 此處:用於暫停播放功能。

function PauseMusic() {
    audio.pause();
    ShowStatus(2);
}

// ****************************************************
// 此處:用於停止播放按鈕功能。
function StopMusic() {
    audio.pause();
    audio.load();
    play.innerText = "4";
    ShowStatus(3);

}


// ****************************************************
// 此處:用於若切換下拉式歌單時，如當時狀態為播放，切換歌曲後繼續播放。

function ChangeMusic(index) {
    song.src = music.options[index].value;
    song.title = music.options[index].innerText;
    console.log(song.title);
    music.options[index].selected = true;
    audio.load();

    if (play.innerText == ";") {
        audio.play();
        ShowStatus(1);
    }
    //PlayMusic();
}

// ****************************************************
// 此處:用於狀態資訊欄功能顯示。

function ShowStatus(status) {
    var strStatus = "";

    switch (status) {
        case 1:
            strStatus = "現正播放：" + song.title;
            break;
        case 2:
            strStatus = "音樂暫停";
            break;
        case 3:
            strStatus = "音樂停止";
            break;
    }

    info.innerText = strStatus;
}