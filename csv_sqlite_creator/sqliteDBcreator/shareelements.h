#ifndef SHAREELEMENTS_H
#define SHAREELEMENTS_H

#include <QFileDialog>
#include <QMessageBox>
#include <QString>
#include "QtSql/QSqlDatabase"
#include "QSqlQuery"
#include <QSettings>
#include <QMap>
#include <QTextStream>

class shareElements
{
public:
    shareElements()=delete;
    static QMessageBox* dialog_message_box;
    static QList<QStringList*>* data_container;
    static QStringList* data_headers;
    static QString iniFileName;
    static QString csvDir_iniParamKey;
    static QString sqliteDbDir_iniParamKey;
    static QString fullIniFilePath;
    static QMap<QString,QString> iniSettingsDict;
    static void  showDialogMsg(QMessageBox::Icon,const QString windowTitle,const QString dialogMsg);
    static void createIniFile();
    static void updateIniFile(QString key, QString value);
    static QString readIniFile();
    static QString getSlash();
private:
    static QString  slash;


};

#endif // SHAREELEMENTS_H
