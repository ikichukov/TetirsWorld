#Tettris Game

Проектната задача се состои од изработка на игра според моделот на популарната игра тетрис. Целта на играта е да се исчистат колку што е можно повеќе редови на коцки преку разместување на различни форми кои се генерираат случајно. Генерираните форми се бираат од осум различни форми и се спуштаат од горе кон доле, сѐ до позиција во која има слободен простор. Дополнително за корисникот се пресметува и број на поени, а со тоа напоредно се врши и повремено зголемување на нивото. Во однос на висината на нивото, се зголемува и тежината на играта, која е изразена преку брзината на спуштање на формите. При стартувањето пак, на играчот му се понудени три начина на играње на играта. Single player - најкласичен начин на играње, единствен играч, се пресметуваат поени и се зголемува ниво. Two player - начин во кој играта може да ја играат двајца играчи. Третиот начин содржи временско ограничување при играњето како предизвик за достигнување на што е можно повеќе поени за ограничено време. Во позадина, кодот е организиран во неколку класи. Притоа секоја од формите има своја класа, кои пак наследуваат од класата **Shape** во која се наоѓаат заеднички методи. Додатни класи кои постојат се класите **Game**, **IndexKeeper** и **CloudAnimation**. Преку класата **Game** се врши главната контрола врз теокот и однесувањето играта. Додека **IndexKeeper** се користи како податочна структура, а **CloudAnimation** за функционирањето на анимациска компонента во формата. Постои и класа **Block** исто така како податочна структура за задачата. 
 
##Опис на решениe

Гланата логика на играта Тетрис се состои од разгледување на целата површина за играње како матрица. Со тоа лесно ќе се манупулира исцртувањето на формите, како и контролата врз чистењето на исполентите редови. Затоа ќе креираме матрица од класата **Bock**, во класата **Game**. Класата во себе чува *булеан* вредност за зафатеноста на полето од матрицата, две *int* вредности со кои се означува позицијата на исцртување на полето, како и *Color*  каде се чува бојата која ја има полето. Освен матрицата класата **Game** во себе содржи и број на редови и колони, ширината на секое поле и почетната брзина на спуштање како *int* вредности. Како *int* вредности се чуваат и поените, бројот на избришани редови, брзината и нивото. Во класата постојат и други елементи кои помагаат при извршувањето на играта. Два тајметри, листа на редови за бришење, два **Graphics** објекта и два **Shape** објекти.