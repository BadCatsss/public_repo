#include <QCoreApplication>
#include <QCoreApplication>
#include <QDebug>
#include "xlsxchartsheet.h"
#include "xlsxcellrange.h"
#include "xlsxchart.h"
#include "xlsxrichstring.h"
#include "xlsxworkbook.h"
#include "SettingsConverter.h"

using namespace QXlsx;
using namespace std;
//https://www.mobile-networks.ru/articles/kody_mobilnyh_operatorov_mira.html
QList<QString> SettingsConverter::ukrOperatorsCodes={"67", "68", "96", "97", "98","50", "66", "95", "99","63","93","91","73"
                                                     ,"39","92","94"};
QList<QString> SettingsConverter::ukrOperatorsCodes1={"067", "068", "096", "097", "098","050", "066", "095", "099","063","093","091","073"
                                                      ,"039","092","094"};
QList<QString> SettingsConverter::ukrOperatorsCodes2={"8067", "8068", "8096", "8097", "8098","8050", "8066", "8095", "8099","8063","8093","8091","8073"
                                                      ,"8039","8092","8094"};
QList<QString> SettingsConverter::azerbaijanOperatorsCodes={"50","55"};
QList<QString> SettingsConverter::BahrainOperatorsCodes={"39","36"};
QList<QString> SettingsConverter::IsraelOperatorsCodes={"54","52"};
QList<QString> SettingsConverter::JordanOperatorsCodes={"78"};
QList<QString> SettingsConverter::IndiaOperatorsCodes={"9810","9818","9871","9935","9936","9906","9932","9933","9934","9937","9938","9862",
                                                       "9954","9931","9910","9939","9971","9955","9956","9958","9957","99570","99571","99572",
                                                       "99573","99574","9897","9997","9896","99960","99961","99962","99963","99964","9890","9860",
                                                       "9960","9970","99750","99751","99752","99753","99754","9898","9998","9974","9892;","9867",
                                                       "9967","9987","9893","9993","99810","99811","99812","99813","99814","9894","9994","9944","99407",
                                                       "99408","99409","99521","99522","99523","99524","9895","9995","9815","9872","9876","9915","9976","9878",
                                                       "9849","9866","9949","9989","9908","9963","9845","9880","9945","9980","9900","9901","9972","9902","9816",
                                                       "9840","99400","99401","99402","99403","99404","99405","99406","99520","9831","9903","9829","9928","9929",
                                                       "99500","99501","99502","99503","99504","9844","9891","9911"};
QList<QString> SettingsConverter::IrakOperatorsCodes={"79"};
QList<QString> SettingsConverter::kyrgyzstanOperatorsCodes={"312","50","55"};
QList<QString> SettingsConverter::KuwaitOperatorsCodes={};
QList<QString> SettingsConverter::LebanonOperatorsCodes={};
QList<QString> SettingsConverter::bhutanOperatorsCodes={};
QList<QString> SettingsConverter::MaldivesOperatorsCodes={"7","96","97","98","99"};
QList<QString> SettingsConverter::NepalOperatorsCodes={"980"};
QList<QString> SettingsConverter::AOEOperatorsCodes={"50","16"};
QList<QString> SettingsConverter::OmanOperatorsCodes={"95","1505","99","92"};
QList<QString> SettingsConverter::QatarOperatorsCodes={"42701","42702"};//https://htmlweb.ru/geo/oper.php?country=QA
QList<QString> SettingsConverter::IranOperatorsCodes={"91"};
QList<QString> SettingsConverter::YemenOperatorsCodes={"71","73"};
QList<QString> SettingsConverter::MongoliaOperatorsCodes={"99"};
QList<QString> SettingsConverter::PakistanOperatorsCodes={"345","346","333","334"};
QList<QString> SettingsConverter::PalestineOperatorsCodes={"59"};
QList<QString> SettingsConverter::Saudi_ArabiaOperatorsCodes={"50","55","53","56"};
QList<QString> SettingsConverter::SyriaOperatorsCodes={"944","947","955","966"};
QList<QString> SettingsConverter::TajikistanOperatorsCodes={"93","92","917"};
QList<QString> SettingsConverter::TurkmenistanOperatorsCodes={"663","664"};
QList<QString> SettingsConverter::TurkeyOperatorsCodes={"50","55","532","533","535","536","537","538","539","534",
                                                        "542","543","544","545","546","549"};
QList<QString> SettingsConverter::uzbekistanOperatorsCodes={"90","91","97","98","93","94","99","92"}; //http://www.mobile.uz/?page_id=3446 //http://operator.lodki-ua.com/uzbekistan
QList<QString> SettingsConverter::Sri_LankaOperatorsCodes={"77","71","72"};
QList<QString> SettingsConverter::gorgiaOperatorsCodes={"514",  "544", "551", "555", "557","558","568", "570","571","574","577","579","591",
                                                        "592", "593","595","596" ,"597" ,"598","599","790","791" };
QList<QString> SettingsConverter::RussianOperatorCodes={"902","950","952","900","903","905","906","909","951","960","961","962","963","964","965","968","969","980","983","986","955"
                                                        ,"997","970","971","995","958","985","933","996",
                                                        "999","954","941","904","908","953","977","930","939","966","981","984","901","989","920","921","922","923","925","926","927"
                                                        ,"928","929","937","910","911","912","913","914","915","916","917","918","919","978","982","983","985","986","987","988"
                                                        ,"912","923","924","931","934","936","967","968","982","983","985","986","988","991","992","993","994","932","938"};
QMap<QString,QPair<int,QList<QString>*>> SettingsConverter::number_matching_rules={
    {"380",QPair<int,QList<QString>*>(12,&SettingsConverter::ukrOperatorsCodes)},
    {"38",QPair<int,QList<QString>*>(12,&SettingsConverter::ukrOperatorsCodes1)},
    {"3",QPair<int,QList<QString>*>(12,&SettingsConverter::ukrOperatorsCodes2)},
    {"7",QPair<int,QList<QString>*>(11,&SettingsConverter::RussianOperatorCodes)},
    {"8",QPair<int,QList<QString>*>(11,&SettingsConverter::RussianOperatorCodes)},
    {"994",QPair<int,QList<QString>*>(12,&SettingsConverter::azerbaijanOperatorsCodes)},
    {"995",QPair<int,QList<QString>*>(13,&SettingsConverter::gorgiaOperatorsCodes)},
    {"973",QPair<int,QList<QString>*>(11,&SettingsConverter::BahrainOperatorsCodes)},
    {"961",QPair<int,QList<QString>*>(11,&SettingsConverter::LebanonOperatorsCodes)},
    {"975",QPair<int,QList<QString>*>(11,&SettingsConverter::bhutanOperatorsCodes)},
    {"973",QPair<int,QList<QString>*>(11,&SettingsConverter::BahrainOperatorsCodes)},
    {"972",QPair<int,QList<QString>*>(12,&SettingsConverter::IsraelOperatorsCodes)},
    {"962",QPair<int,QList<QString>*>(12,&SettingsConverter::JordanOperatorsCodes)},
    {"91",QPair<int,QList<QString>*>(12,&SettingsConverter::IndiaOperatorsCodes)},
    {"964",QPair<int,QList<QString>*>(13,&SettingsConverter::IrakOperatorsCodes)},
    {"996",QPair<int,QList<QString>*>(12,&SettingsConverter::kyrgyzstanOperatorsCodes)},
    {"965",QPair<int,QList<QString>*>(11,&SettingsConverter::KuwaitOperatorsCodes)},
    {"960",QPair<int,QList<QString>*>(10,&SettingsConverter::MaldivesOperatorsCodes)},
    {"977",QPair<int,QList<QString>*>(13,&SettingsConverter::NepalOperatorsCodes)},
    {"971",QPair<int,QList<QString>*>(12,&SettingsConverter::AOEOperatorsCodes)},
    {"968",QPair<int,QList<QString>*>(11,&SettingsConverter::OmanOperatorsCodes)},
    {"974",QPair<int,QList<QString>*>(11,&SettingsConverter::QatarOperatorsCodes)},
    {"98",QPair<int,QList<QString>*>(12,&SettingsConverter::IranOperatorsCodes)},
    {"967",QPair<int,QList<QString>*>(12,&SettingsConverter::YemenOperatorsCodes)},
    {"976",QPair<int,QList<QString>*>(11,&SettingsConverter::MongoliaOperatorsCodes)},
    {"92",QPair<int,QList<QString>*>(12,&SettingsConverter::PakistanOperatorsCodes)},
    {"970",QPair<int,QList<QString>*>(12,&SettingsConverter::PalestineOperatorsCodes)},
    {"966",QPair<int,QList<QString>*>(12,&SettingsConverter::Saudi_ArabiaOperatorsCodes)},
    {"963",QPair<int,QList<QString>*>(12,&SettingsConverter::SyriaOperatorsCodes)},
    {"992",QPair<int,QList<QString>*>(12,&SettingsConverter::TajikistanOperatorsCodes)},
    {"993",QPair<int,QList<QString>*>(12,&SettingsConverter::TurkmenistanOperatorsCodes)},
    {"90",QPair<int,QList<QString>*>(12,&SettingsConverter::TurkeyOperatorsCodes)},
    {"998",QPair<int,QList<QString>*>(12,&SettingsConverter::uzbekistanOperatorsCodes)},
    {"94",QPair<int,QList<QString>*>(11,&SettingsConverter::Sri_LankaOperatorsCodes)}
};
QMap<QString,int> SettingsConverter::otherCountries1={
    {"7",11},{"380",12},{"77",11},{"7940",11},{"370",11},{"375",12},{"62",11},
    {"1",11},{"61",11},{"43",12},{"355",12},{"213",12},{"244",12},{"376",9},{"1268",11},{"54",13},{"374",11},
    {"297",10},{"93",11},{"1242",11},{"880",13},{"1246",11},{"501",10},{"32",11},{"229",11},
    {"225",11},{"1441",11},{"359",12},{"591",11},{"387",11},{"267",11},{"55",12},{"1284",11},{"673",10},
    {"226",11},{"257",11},{"678",10},{"44",12},{"36",11},{"58",12},{"670",11},{"84",11},{"241",11},
    {"509",11},{"220",10},{"233",12},{"590",12},{"502",11},{"224",11},{"245",10},{"49",12},{"350",11},{"852",11},
    {"504",11},{"1473",11},{"299",9},{"30",12},{"671",11},{"45",10},{"1767",11},{"1809",11},{"20",12},
    {"260",12},{"263",12},{"353",12},{"354",10},
    {"34",11},{"39",12},{"1345",11},{"855",11},{"237",11},{"1",11},{"238",10},{"254",12},
    {"357",11},{"86",13},{"57",12},{"269",10},{"242",12},{"383",10},{"506",11},{"53",10},
    {"599",11},{"371",11},{"266",11},{"231",10},{"21",12},{"423",12},{"352",12},{"230",10},{"222",11},
    {"261",12},{"853",11},{"389",11},{"265",12},{"60",11},{"223",11},{"356",11},{"212",12},{"596",12},
    {"52",13},{"258",12},{"373",11},{"377",12},{"381",11},{"264",12},{"674",10},{"277",11},
    {"234",13},{"31",11},{"505",11},{"64",11},{"687",9},{"47",10},{"682",8},
    {"507",11},{"675",10},{"595",12},{"51",11},{"48",11},{"351",12},{"1787",11},{"262",12},
    {"250",12},{"40",11},{"685",9},{"378",11},{"1758",11},{"82",13},{"670",11},{"248",10},{"221",12},
    {"1784",11},{"1869",11},{"381",11},{"65",10},{"421",12},{"386",11},{"677",10},{"249",12},{"597",10},
    {"232",11},{"66",11},{"886",12},{"255",12},{"228",11},{"676",10},{"1868",11},{"216",11},
    {"256",12},{"598",11},{"298",9},{"679",10},{"63",12},{"358",12},{"33",11},{"594",12},
    {"689",9},{"385",11},{"236",11},{"235",11},{"420",12},{"56",11},{"41",11},{"46",11},{"593",12},
    {"240",12},{"503",11},{"372",11},{"251",12},{"27",11},{"82",12},{"1876",11},{"81",12}};

QStringList SettingsConverter::Createdtables;
QStringList SettingsConverter::invalidCharacters={ "~", "@", "#", "$", "%", "^", "-", "_", "(", ")", "{", "}", "`", "+", "=", "[", "]", ":",
                                                   ",", ";",",", ".", "/", "?","/","\\",":","*","?","«","»","<",">","|","&","—","\""," ","'"};

bool SettingsConverter::writeToDb(QString DB_name,QString DB_host,
                                  QString userLogin,QString userPassword,
                                  QStringList* insertQueries,QStringList* selectQueries,
                                  QStringList* updateQueries
                                  )
{  
    static  QSqlDatabase dbObj;

    if(!dbObj.contains("MyDB1")) {
        dbObj = QSqlDatabase::addDatabase("QMYSQL", "MyDB1");
    }

    dbObj.setHostName(DB_host);
    dbObj.setDatabaseName(DB_name);
    dbObj.setUserName(userLogin);
    dbObj.setPassword(userPassword);

    if (dbObj.open()) {
        for(int i=0;i<globalDBOperations.length();i++)
        {
            QString  queryStr=globalDBOperations[i];
            QSqlQuery currentQuery(dbObj);

            if (!currentQuery.exec(queryStr)) {

                qDebug()<<"kek"<<dbObj.lastError()<<currentQuery.lastError()<<endl;
                return false;
            }
        }
        if(insertQueries->length()>0)
        {
           // qDebug()<<"Succesfull connect to database"<<endl;
            insertQueries->removeDuplicates();
            //insert
            if (selectQueries==nullptr) {
                for(QString& queryStr : *insertQueries)
                {
                    QSqlQuery currentQuery(dbObj);
                    if (!currentQuery.exec(queryStr)) {

                        qDebug()<<dbObj.lastError()<<currentQuery.lastError()<<endl;
                        return false;
                    }
                }
            }
            else //select and insert
            {
                selectQueries->removeDuplicates();
                for(int i=0;i<selectQueries->length();i++)
                {
                    QString  queryStr=(*selectQueries)[i];
                    QSqlQuery currentQuery(dbObj);

                    currentQuery.exec(queryStr);
                    if (  currentQuery.size()==0) { // insert new
                        QString  queryStr=(*selectQueries)[i];
                        QSqlQuery currentQuery1(dbObj);
                        currentQuery1.exec(queryStr);
                    }
                    else //merge
                    {
                        for(int i=0;i<updateQueries->length();i++)
                        {
                            QString  queryStr=(*updateQueries)[i];
                            QSqlQuery currentQuery1(dbObj);
                            if(  currentQuery1.prepare(queryStr))
                            {
                                currentQuery1.exec();
                            }
                            else{
                                qDebug()<<dbObj.lastError()<<currentQuery1.lastError()<<endl;
                            }
                        }
                    }

                    //                if(dbObj.)
                    //                {
                    //                    qDebug()<<dbObj.lastError()<<endl;
                    //                    return false;
                    //                }
                }
            }
            return true;
        }
        return false;
    }
}

QStringList SettingsConverter::prepareQueries(QList<QStringList*> *values, SettingsConverter::DB_operationEnum operation,
                                              QList<QPair<QString,QString>> atrib,QString tableName,QList<QPair<QString,int>>* whereParams,QList<int>* updateIndexes)
{
    static int callCount=0;
    QString brackets="`";
    QString brackets1="\"";
    QStringList operations;
    if (callCount==0) {
        globalDBOperations.append("USE "+brackets+databaseName+brackets+";");
        globalDBOperations.append("SET NAMES '"+tableEncoding+"'"+";");
        globalDBOperations.append("SET CHARACTER SET "+brackets+characterEncoding+brackets+";");
        globalDBOperations.append("SET SESSION collation_connection = "+brackets+collate+brackets+";");
        globalDBOperations.append("ALTER DATABASE"+brackets+databaseName+brackets+" CHARACTER SET "+characterEncoding+ " COLLATE " +collate+";");
        globalDBOperations.append("SET SQL_SAFE_UPDATES = 0;");
        QString createTableQuery="CREATE TABLE IF NOT EXISTS "+brackets+Createdtables[0]+brackets;
        createTableQuery+=" ( ";
        for(int i=0; i<atrib.length();i++)
        {
            createTableQuery+= brackets+  atrib[i].first+brackets+" "+atrib[i].second+" CHARACTER SET "+characterEncoding+" COLLATE "+collate+
                    ", ";
        }
        createTableQuery+=" INDEX USING BTREE("+tablesAtrib.at(2).first+")";
        //createTableQuery.remove(createTableQuery.lastIndexOf(","),1);
        createTableQuery+=" );";
        globalDBOperations.append(createTableQuery);

        callCount++;
    }

    QString whereSubQuery="";
    QList<QString> whreres_;
    if ( whereParams!=nullptr) {
        for (int var = 0; var < values->length(); ++var)
        {
            whereSubQuery=" WHERE ";
            bool isWhereNeed=false;
            for(auto pair:*whereParams)
            {
                if(values->at(var)->at(pair.second)!="  ")
                {
                    whereSubQuery+=brackets+pair.first+brackets+"="+brackets1+ values->at(var)->at(pair.second)+brackets1+" OR ";
                    isWhereNeed=true;
                }
            }
            if (!isWhereNeed) {
                whereSubQuery="";
            }
            whreres_.push_back(  whereSubQuery= whereSubQuery.remove(whereSubQuery.lastIndexOf("OR"),2));
        }

    }
    switch(operation)
    {
    case  _insert:
        for (int var = 0; var < values->length(); ++var)
        {
            QString q;
            q="INSERT INTO "+brackets+tableName+brackets+" ( ";
            for (auto& element : atrib)
            {
                q+=brackets+element.first+brackets+",";
            }
            q.remove(q.length()-1,1);
            q+=")";
            q+=" VALUES ( ";
            for (int j = 0; j < values->at(var)->length(); ++j)
            {
                q+=brackets1+(*(*values)[var])[j]+brackets1+",";
            }
            if (values->at(var)->length()<atrib.length()) {
                for (int p = values->at(var)->length(); p < atrib.length(); ++p)
                {
                    q+=brackets1+" "+brackets1+",";
                }
            }
            q.remove(q.lastIndexOf(","),1);
            q+=");";
            operations.append(q);
        }
        break;
    case _select:
    {
        for (int var = 0; var < values->length(); ++var)
        {
            for (int var1 = 0; var1 < values->length(); ++var1)
            {
                QString q="";
                q="SELECT  ";
                for (auto& element : atrib)
                {
                    q+=element.first+",";
                }
                q.remove(q.lastIndexOf(","),1);

                q+= " FROM "+brackets+tableName+brackets+" "+whreres_.at(var1)+";";
                operations.append(q);
            }
        }
    }
        break;
    case _update:
    {
        if(updateIndexes!=nullptr)
        {
            QString q="";
            int k=0;
            int j=0;
            for(int i=0; i<values->length();i++)
            {
                q+= "UPDATE "+brackets+tableName+brackets+" SET ";
                for(;j<atrib.length();j++)
                {
                    q+=brackets+atrib[j].first+brackets+"="+brackets1+values->at(i)->at(updateIndexes->at(j))+brackets1;
                    for(;k<whreres_.length();k++)
                    {
                        q+=whreres_.at(k);
                        q=q.simplified();
                        q+=";";
                        operations.append(q);
                        q.clear();
                        break;
                    }
                    q+= "UPDATE "+brackets+tableName+brackets+" SET ";
                    continue;
                }
            }
        }
    }

        break;
    case _del:
        QString q;
        q="DELETE  ";
        for (auto& element : atrib)
        {
            q+=element.first+",";
        }
        q.remove(q.length()-1);
        q+= "FROM "+databaseTableName+" "+whereSubQuery;
        operations.append(q);
        break;
    }

    return operations;
}
void SettingsConverter::logIncorrectNumbers(QFile* badRowsF, QString value)
{
    if (badNumbersLogF==nullptr || maybeBadNumbersLogF==nullptr) {
        badNumbersLogF = new QFile("BadNumbers_"+databaseTableName+".txt");
        maybeBadNumbersLogF = new QFile("MaybeBadNumbers_"+databaseTableName+".txt");
    }
//    if (badRowsF->open(QIODevice::WriteOnly | QIODevice::Append))
//    {

//        badRowsF->write( value.toStdString().c_str());
//        badRowsF->write("\r\n");
//        badRowsF->close();
//        // qDebug()<<"INCORRECT WAS WRITE: "<<value.toStdString().c_str()<<endl;
//    }
}
//book and sheet setup
bool SettingsConverter::openFile()
{
    string tmpString = openedFilePath.toStdString();
    int startSearchPosition = tmpString.find(".");
    databaseTableName=openedFilePath.mid(openedFilePath.lastIndexOf("\\")+1,openedFilePath.lastIndexOf(".")-openedFilePath.lastIndexOf("\\")-1);
    databaseName="vk_merge";
    Createdtables.append(databaseTableName);
    if (tmpString.find("xlsx",startSearchPosition) != std::string::npos) {
        xlsxDoc = new QXlsx::Document(openedFilePath);

        if (this->xlsxDoc->load())
        {
            auto sheetList = this->xlsx_getSheetsList();
            this->xlsx_setActivetWorkSheet(sheetList[0]);
            fileIsXlsx=true;
            fileColsCorrectNumber=8;
            return true;
        }
        else {
            return false;
        }
    }
    else if(tmpString.find("txt",startSearchPosition) != std::string::npos) {
        fileIsTxt=true;
        fileColsCorrectNumber=4;
        txtDoc = new QFile(openedFilePath);
        return true;
    }
    else{return false;}
}
void SettingsConverter::checkNumberOperatorCode(QList<QStringList*>& list,int correctColsCount,int isdnIndex,bool needRemovePrefix)
{
    // normalNumberRegExp = new QRegExp("((995)\\d{8,13})|((7|8)\\d{8,10}|(9)\\d{8,10}|(380)\\d{8,10})");//normal
    manySameDigitNumberRegExp=new QRegExp("(\\d{0,}?)(\\d)\\2{3,}(\\d{0,})"); // 223322223 , 9000000001 , 9038888888, 5555555555, 9998885555
    sequenceNumberRegExp= new QRegExp("^((?:(?=01|12|23|34|45|56|67|78|89)\\d)+\\d)|((?:(?=98|87|76|65|54|43|32|21|10)\\d)+\\d)$");//123456789 //98765432
    //TEST
    //additionalNumberCheck("9033333387");
    //additionalNumberCheck("9033288882");
    //END TEST
    badNumbersLogF = new QFile("BadNumbers_"+databaseTableName+".txt");
    maybeBadNumbersLogF = new QFile("MaybeBadNumbers_"+databaseTableName+".txt");
    static int debugCount1=0;
    for (auto& d : list)
    {
//        qDebug()<<debugCount1<<endl;
//        debugCount1++;
        if (d->length()==correctColsCount) {
            for (int var = 0; var < correctColsCount; ++var) {
                const_cast<QString*>(&d->at(var))->replace(  d->at(var),d->at(var).trimmed());
                const_cast<QString*>(&d->at(var))->replace(  d->at(var),d->at(var).simplified());
                if (d->at(var)=="" || d->at(var).isEmpty() || d->at(var)==NULL ||  d->at(var)==nullptr) {
                    const_cast<QString*>(&d->at(var))->replace(  d->at(var),"  ");
                }
            }
            d->removeAll("");
            QStringList numbers=d->at(isdnIndex).split(",");
            for (int k = 0; k < numbers.length(); ++k) {
                bool isAppended=false;
                for (int var = 0; var < invalidCharacters.length(); ++var) {
                    const_cast<QString*>(&numbers.at(k))->remove(invalidCharacters.at(var)) ;
                }
                auto  it= number_matching_rules.begin();
                while (it!=number_matching_rules.end()) {
                    if (numbers.at(k).startsWith(it.key())) { //start with country code
                        if (numbers.at(k).length()==it.value().first) {
                            isNumbersPassRule.insert(k,true);
                            foundedisdnPrefix.insert(it.key());
                            isAppended=true;
                            otherNumberLength.insert(k,it.value().first);
                        }
                    }
                    else { //maybe start with mobile operator code
                        for (int l = 0; l < it.value().second->length(); ++l) {
                            if (numbers.at(k).startsWith(it.value().second->at(l)) && numbers.at(k).length()==it.value().first-it.key().length()) {
                                const_cast<QString*>(&numbers.at(k))->replace( numbers.at(k),it.key()+numbers.at(k));
                                isNumbersPassRule.insert(k,true);
                                foundedisdnPrefix.insert(it.key());
                                isAppended=true;
                                otherNumberLength.insert(k,it.value().first);
                            }
                        }
                    }
                    if (!isAppended && isNumbersPassRule.length()<numbers.length()) {
                        isNumbersPassRule.insert(k,false);
                        otherNumberLength.insert(k,0);
                    }
                    it++;
                }
            }
            checkAndRemoveBadNumber(isdnIndex,&numbers,d,list,needEraseBadIsdn);
            if (d->size()>0 && needRemovePrefix) {
                for(const QString& prefix : foundedisdnPrefix)
                {
                    const_cast<QString&>(d->at(isdnIndex)).remove(prefix);
                }
            }
            foundedisdnPrefix.clear();
            isNumbersPassRule.clear();
            otherNumberLength.clear();
        }
        else {
            QString tmpStr;
            for (int var = 0; var < d->length(); ++var) {
                tmpStr+=d->at(var);
            }
            logIncorrectNumbers(maybeBadNumbersLogF,tmpStr);
            list.removeOne(d);
        }
    }
}
void SettingsConverter::checkAndRemoveBadNumber( int isdnIndex,QStringList *numbersList, QStringList *var,QList<QStringList*>& list,QList<bool> needErase)
{
    int k=0;
    static  int eraseIndex=0;
    if(eraseIndex>needErase.length()-1)
    {
        eraseIndex=0;
    }
    foreach (QString number, *numbersList) {
//        qDebug()<< number<<endl;
//        qDebug()<< (bool)(number.length()!=otherNumberLength.at(k))<<endl;
//        qDebug()<<!isNumbersPassRule.at(k)<<endl;
        if (
                (   ((manySameDigitNumberRegExp->exactMatch(number)||

                      sequenceNumberRegExp->exactMatch(number)
                      ||number==""
                      ||number.isEmpty()
                      ||number==" "
                      || (!isNumbersPassRule.at(k) && number.length()!=otherNumberLength.at(k))
                      || !additionalNumberCheck(number))
                     )))
        {

            if (numbersList->length()==1) {
                QString tmpStr;
                for (int l = 0; l < var->length(); ++l)
                {
                    tmpStr+=var->at(l)+";";
                }

                if (needErase.at(eraseIndex)) {
                    list.removeOne(var);
                    var->clear();
                    numbersList->clear();
                    //tmpStr+="bad isdn and email. not included in db;";
                      logIncorrectNumbers(badNumbersLogF,tmpStr);
                }
                else
                {
                    const_cast<QString&>(var->at(isdnIndex))="  ";
                     // tmpStr+="bad isdn , good email.  included in db;";
                        //logIncorrectNumbers(badNumbersLogF,tmpStr);
                }
              //  logIncorrectNumbers(badNumbersLogF,tmpStr);
            }
            else
            {
                numbersList->removeOne(number);
            }
        }

        k++;
        eraseIndex++;
    }


}
bool SettingsConverter::isStringNormal(QString &str, QRegExp rule)
{
    if (str=="" || str.isEmpty() || str==NULL || str==nullptr) {
        const_cast<QString*>(&str)->replace(  str,"  ");
    }
    if(rule.exactMatch(str))
    {
        return false;
    }
    else{
        return true;
    }
}

void SettingsConverter::xlsx_calculateNotEmptyRowsCount( )
{
    this->xlsx_foundRowsCount =  this->xlsx_foundColumnsCount = xlsxDoc->dimension ().lastRow()+1;
    cout << "row " << this->xlsx_foundRowsCount << endl;
}
void SettingsConverter::xlsx_calculateNotEmptyColumnsCount( )
{

    this->xlsx_foundColumnsCount = xlsxDoc->dimension ().lastColumn ()+1;
    cout << "col " << this->xlsx_foundColumnsCount << endl;
}
void SettingsConverter::xlsx_setActivetWorkSheet(const QString& chosenSheet)
{
    this->xlsx_activeSheet = this->xlsxDoc->sheet(chosenSheet);
    this->xlsxDoc->selectSheet(this->xlsx_activeSheet->sheetName());
}
QStringList  SettingsConverter::xlsx_getSheetsList()
{
    cout << "Sheet is found" << endl;
    QStringList sheetList;
    QTextStream qtout(stdout);
    for (auto sheet : this->xlsxDoc->sheetNames())
    {
        sheetList.push_back(sheet);
    }
    return sheetList;
}

//parse
SettingsConverter::SettingsConverter(const QString& fileName,const QString& clasterName, QList<QPair<QString,QString>>  tables_atribs,  QList<QPair<int,QRegExp>>* additionalIndexForCheck)
{
    openedFilePath=fileName;
    fileClaster=clasterName;
    tablesAtrib=tables_atribs;
    this->additionalIndexForCheck=new  QList<QPair<int,QRegExp>>();
    if (!QDir("BadData").exists()) {
        QDir().mkdir("BadData");
    }
    //tableName=filePath.mid(filePath.lastIndexOf("\\")+1).remove(".xlsx");
    badNumbersLogF = new QFile("BadData\\"+databaseTableName+"Bad.txt");
}
void SettingsConverter::chekToNeedEraseIndexes()
{
    needEraseBadIsdn.clear();
    if (additionalIndexForCheck != nullptr)
    {
        for (int i = 0; i < additionalIndexForCheck->length(); i++)
        {
            for(int j=0;j<values.length();j++)
            {
                auto d =values.at(j)->at(additionalIndexForCheck->at(i).first);
                needEraseBadIsdn.append(isStringNormal(d,additionalIndexForCheck->at(i).second));
            }
        }
    }
    else
    {
        for(int j=0;j<values.length();j++)
        {

            needEraseBadIsdn.append(true);
        }
    }
}
bool SettingsConverter:: processData()
{

    if (fileIsXlsx) {
        this->xlsx_calculateNotEmptyRowsCount();
        this->xlsx_calculateNotEmptyColumnsCount();
        additionalIndexForCheck->clear();
        additionalIndexForCheck->append(QPair<int,QRegExp>(7,QRegExp("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")));
        if ( !this->xlsx_readFile()) {
            return false;
        }
        else
        {

            chekToNeedEraseIndexes();
            checkNumberOperatorCode(values,8,6,needRemoveIsdnPrefix);
            QList< QPair<QString,QString>> selectAtribs;
            selectAtribs.append(QPair<QString,QString>("COUNT(*)",""));
            QList< QPair<QString,QString>> insertAtribs;
            insertAtribs.append(QPair<QString,QString>("id","varchar(20)"));
            insertAtribs.append(QPair<QString,QString>("url","varchar(400)"));
            insertAtribs.append(QPair<QString,QString>("was_banned","varchar(20)"));
            insertAtribs.append(QPair<QString,QString>("date_of_birth","varchar(20)"));
            QList<QPair<QString,int>> whereRules;
            QList<int> updateIndexes;
            whereRules.append(QPair<QString,int>("isdn",6));
            whereRules.append(QPair<QString,int>("email",7));

            //TODO - rewrite updateIndexes appends to separate method where appends to updateIndexes was automaticaly
            //in accordance with insertAtribs
            updateIndexes.append(0); //insertAtribs 0 param index at values arr
            updateIndexes.append(1);//insertAtribs 2 param index at values arr
            updateIndexes.append(4);//insertAtribs 3 param index at values arr
            updateIndexes.append(5);//insertAtribs 4 param index at values arr
            //




            auto insertQ = prepareQueries(&values,DB_operationEnum::_insert,insertAtribs,Createdtables[0]);
            auto selectQ = prepareQueries(&values,DB_operationEnum::_select,selectAtribs,Createdtables[0],&whereRules);
            auto updateQ = prepareQueries(&values,DB_operationEnum::_update,insertAtribs,Createdtables[0],&whereRules,&updateIndexes);

            //writeToDb(databaseName,db_host,db_user,db_pass,&insertQ,&selectQ,&updateQ);

        }
    }
    else if(fileIsTxt)
    {
        additionalIndexForCheck->clear();
        additionalIndexForCheck->append(QPair<int,QRegExp>(0,QRegExp("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")));//email txt doc
        this->txt_readFile();
    }
    else {
        return false;
    }
    return true;
}

bool SettingsConverter::txt_readFile()
{
    static int debugCount=0;
    if (txtDoc->open(QIODevice::ReadOnly))
    {
        QString readBufferString;
        int alreadyReadRowsCount=0;
        int maxRowsForRead=100;
        char globalDelimetr='\n';
        char delimetr=',';
        while (!txtDoc->atEnd()) {

            if (alreadyReadRowsCount>=maxRowsForRead) {
readF:
                QStringList globalStringsList=readBufferString.split(globalDelimetr);
                foreach (const QString& d, globalStringsList)
                {
                    const_cast<QString&>(d).replace("\n","");
                }
                globalStringsList.removeAll("\n");
                globalStringsList.removeAll("");
                for(QString substr : globalStringsList )
                {
                    substr=substr.replace("'","");
                    if (substr.split(delimetr).length()==fileColsCorrectNumber) {
                        values.append(new QStringList( substr.split(delimetr)));
                    }
                    else {
                        QString  tmpStr;
                        QStringList tmpList= substr.split(delimetr);
                        for(int i=0;i<tmpList.length();i++)
                        {
                            tmpStr+=tmpList.at(i)+";";
                        }
                        logIncorrectNumbers(maybeBadNumbersLogF,tmpStr);
                    }
                }
                chekToNeedEraseIndexes();
                debugCount++;
                qDebug()<<debugCount<<endl;
                if (debugCount==21) {
                      qDebug()<<debugCount<<endl;
                }
                // else{needEraseBadIsdn;}

                checkNumberOperatorCode(values,4,2,needRemoveIsdnPrefix);
                auto v = prepareQueries(&values,DB_operationEnum::_insert,tablesAtrib,databaseTableName);
                //writeToDb(databaseName,db_host,db_user,db_pass,&v);
                readBufferString.clear();
                alreadyReadRowsCount=0;
                for(int i=0;i<values.length();i++)
                {
                    delete values[i];
                }
                values.clear();
            }
            else
            {
                readBufferString+=  txtDoc->readLine();
                readBufferString.simplified();
                alreadyReadRowsCount++;
            }
        }
        if(!readBufferString.isEmpty())
        {
            goto readF;
        }
        return  true;
    }
    else{return false;}

}
bool SettingsConverter:: xlsx_readFile()
{
    for ( int r = 1; r < xlsx_foundRowsCount; ++r) {
        values.append(new QStringList());

        for (  int c = 1; c < xlsx_foundColumnsCount; ++c) {
            values.at(r-1)->append(xlsxDoc->read(r,c).toString().remove("'"));

        }
        if ( values.at(r-1)->length()!=fileColsCorrectNumber) {
            QString  tmpStr;

            for(int i=0;i<values.at(r-1)->length();i++)
            {
                tmpStr+=values.at(r-1)->at(i)+";";
            }
            logIncorrectNumbers(maybeBadNumbersLogF,tmpStr);
            values.removeAt(r-1);
        }
    }
    return  true;
}
bool SettingsConverter::additionalNumberCheck(QString number)
{
    int testCount=0;
    int maxRepeat=4;
    for (int var = 0; var < number.length()-1; ++var)
    {
        if (number.at(var)==number.at(var+1)) {
            testCount++;
            if (testCount==maxRepeat) {
               // qDebug()<<"bad "<<number <<endl;
                return  false;
            }
        }
        else{
            testCount=0;
        }
    }
    return true;
}

//bool SettingsConverter::checkDuplicates(QList<QStringList *> &data)
//{
//    QStringList numbers;
//    foreach (auto d, data)
//    {
//        numbers.append( d->at(isdnIndex));
//    }
//    if (numbers.removeDuplicates()>0) {
//        return  true;
//    }
//    else
//    {
//        return false;
//    }
//}

//void SettingsConverter::mergeData(QList<QStringList *>& data1, QList<QStringList *>& data2)
//{
//    QList<QStringList *>* container1=&data1;
//    QList<QStringList *>* container2=&data2;
//    if (data1.length()>data2.length()) {
//        container1=&data1;
//        container2=&data2;
//    }
//    else
//    {
//        container1=&data2;
//        container2=&data1;
//    }

//    foreach (auto numberData2, *container1)
//    {
//        foreach (auto numberData1, *container2)
//        {
//            if (numberData2->contains(numberData1->at(isdnIndex))) {
//                numberData1->append(numberData2->at(numberData2->length()-1));

//            }
//        }
//    }

//}

