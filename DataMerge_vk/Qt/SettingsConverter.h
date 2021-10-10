#pragma once
#include <iostream>
#include <iomanip>
#include <QMap>
#include <QtCore>
#include <QJsonArray>
#include <QtSql>
#include "xlsxdocument.h"
#include <windows.h> // for Sleep

using namespace std;



class SettingsConverter
{   
    static QList<QString> ukrOperatorsCodes;
    static QList<QString> ukrOperatorsCodes1;
    static QList<QString> ukrOperatorsCodes2;
    static QList<QString> gorgiaOperatorsCodes;
    static QList<QString> azerbaijanOperatorsCodes;
    static QList<QString> BahrainOperatorsCodes;
    static QList<QString> LebanonOperatorsCodes;
    static QList<QString> bhutanOperatorsCodes;
    static QList<QString> IsraelOperatorsCodes;
    static QList<QString> JordanOperatorsCodes;
    static QList<QString> IndiaOperatorsCodes;
    static QList<QString> IrakOperatorsCodes;
    static QList<QString> kyrgyzstanOperatorsCodes;
    static QList<QString> KuwaitOperatorsCodes;
    static QList<QString> MaldivesOperatorsCodes;
    static QList<QString> NepalOperatorsCodes;
    static QList<QString> AOEOperatorsCodes;
    static QList<QString> OmanOperatorsCodes;
    static QList<QString> QatarOperatorsCodes;
    static QList<QString> IranOperatorsCodes;
    static QList<QString> YemenOperatorsCodes;
    static QList<QString> MongoliaOperatorsCodes;
    static QList<QString> PakistanOperatorsCodes;
    static QList<QString> PalestineOperatorsCodes;
    static QList<QString> Saudi_ArabiaOperatorsCodes;
    static QList<QString> SyriaOperatorsCodes;
    static QList<QString> TajikistanOperatorsCodes;
    static QList<QString> TurkmenistanOperatorsCodes;
    static QList<QString> TurkeyOperatorsCodes;
    static QList<QString> uzbekistanOperatorsCodes;
    static QList<QString> Sri_LankaOperatorsCodes;
    static QList<QString> RussianOperatorCodes;

    static QMap<QString,QPair<int,QList<QString>*>> number_matching_rules;
    static QMap<QString,int> otherCountries1; //tmp - need rewrite and add to number_matching_rules

    size_t xlsx_foundRowsCount = 0;
    size_t xlsx_foundColumnsCount = 0;
    QXlsx::AbstractSheet* xlsx_activeSheet = nullptr;
    bool fileIsTxt=false;
    bool fileIsXlsx=false;
    QXlsx::Document* xlsxDoc = nullptr;
    QFile* txtDoc=nullptr;
    QFile* badNumbersLogF=nullptr;
    QFile* maybeBadNumbersLogF=nullptr;
    QList<bool> needEraseBadIsdn;
    bool needRemoveIsdnPrefix=false;
    bool isStringNormal(QString& str,QRegExp rule);

    QString openedFilePath = "";
    QString fileClaster="";
    QList<QStringList*> values;
    //number check rules
    QList<int> otherNumberLength;
    QRegExp* manySameDigitNumberRegExp; // 223322223 , 9000000001 , 9038888888, 5555555555, 9998885555
    QRegExp* sequenceNumberRegExp;//123456789 //98765432
    QList<bool> isNumbersPassRule;
    QSet<QString> foundedisdnPrefix;
    QList<QPair<int,QRegExp>>* additionalIndexForCheck=nullptr;

    static QStringList invalidCharacters;
    int fileColsCorrectNumber=0;
    //xlsx
    void xlsx_calculateNotEmptyRowsCount();
    void xlsx_calculateNotEmptyColumnsCount();
    bool xlsx_readFile();
    //txt

    bool txt_readFile();
    // number checking methods
    void logIncorrectNumbers(QFile* badNumbersLogF, QString value);
    void checkNumberOperatorCode(QList<QStringList*>& list,int colsCount,int isdnIndex,bool needRemovePrefix);
    void checkAndRemoveBadNumber( int isdnIndex,QStringList *numbersList, QStringList *var,QList<QStringList*>& list, QList<bool> needErase);
    bool additionalNumberCheck(QString number);
    void chekToNeedEraseIndexes();

    //work with many data set
    //  bool checkDuplicates(QList<QStringList*>& data);
    // void mergeData(QList<QStringList*>& data1,QList<QStringList*>& data2);
    // QList<QString> duplicateList;

    //database
    enum DB_operationEnum
    {
        _insert=0,
        _select=1,
        _update=2,
        _del=3
    }
    ;
    QStringList globalDBOperations;
    static  QStringList Createdtables;
    bool writeToDb(QString DB_name,QString DB_host,
                   QString userLogin,QString userPassword,
                   QStringList* insertQueries,
                   QStringList* selectQueries=nullptr,QStringList* updateQueries=nullptr
            );
    QStringList prepareQueries(QList<QStringList*> *values, SettingsConverter::DB_operationEnum operation,
                               QList<QPair<QString,QString>> atrib,QString tableName,QList<QPair<QString,int>>* whereParams=nullptr,QList<int>* updateIndexes=nullptr);
    QString databaseTableName;
    QString databaseName;
    QList<QPair<QString,QString>> tablesAtrib;

    QString db_host="127.0.0.1";
    QString db_user="root";
    QString db_pass="12345678910abcD";

    QString tableEncoding="utf8mb4";
    QString characterEncoding="utf8mb4";
    QString collate="utf8mb4_unicode_ci";

//    QString tableEncoding="cp1251";
//    QString characterEncoding="cp1251";
//    QString collate="cp1251_general_ci";
public:
    bool processData();
    bool openFile();
    void xlsx_setActivetWorkSheet(const QString&);
    QStringList  xlsx_getSheetsList();
    SettingsConverter(const QString&,const QString&, QList<QPair<QString,QString>> tableSettings, QList<QPair<int,QRegExp>>* additionalIndexForCheck=nullptr);
};



