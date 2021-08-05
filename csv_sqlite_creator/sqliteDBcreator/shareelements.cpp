#include "shareelements.h"

QList<QStringList*>* shareElements::data_container= nullptr;
QStringList* shareElements::data_headers= nullptr;
QMessageBox* shareElements::dialog_message_box= nullptr;
QString shareElements::iniFileName="csvToSqlite_config.ini";
QString shareElements::slash="\\";
QString shareElements::csvDir_iniParamKey="csvFileDirectory";
QString shareElements::sqliteDbDir_iniParamKey="sqliteDbFileDirectory";
QString shareElements::fullIniFilePath= QDir::currentPath()+slash+iniFileName;
QMap<QString,QString> shareElements::iniSettingsDict;
QString shareElements::getSlash()
{
#ifdef Q_OS_WIN
 shareElements::slash="\\";
#elif defined (Q_OS_LINUX)
 shareElements::slash="/";
#endif
 return shareElements::slash;
}

void  shareElements::showDialogMsg(QMessageBox::Icon msgIcon,const QString windowTitle,const QString errorMsg)
{
    dialog_message_box= new QMessageBox (msgIcon,windowTitle,errorMsg,QMessageBox::StandardButton::Ok);
    dialog_message_box->show();
    //delete error_message_box;
}

QString shareElements::readIniFile()
{
    if (QFile::exists(fullIniFilePath)) {
        QFile f(fullIniFilePath);
        f.open(QIODevice::ReadOnly);
        QTextStream in_stream(&f);
        while (!in_stream.atEnd()) {
            auto list=in_stream.readLine().split("=");
            if (list.length()>=2) {
                iniSettingsDict.insert(list[0],list[1]);
            }
        }
        return "";
    }
    else{
        createIniFile();
        return "";
    }
}

void shareElements::createIniFile()
{
    QSettings iniSettings(fullIniFilePath, QSettings::IniFormat);
    iniSettings.setValue(csvDir_iniParamKey, QDir::currentPath());
    iniSettings.setValue(sqliteDbDir_iniParamKey, QDir::currentPath());
    iniSettings.sync();
    iniSettingsDict.insert(csvDir_iniParamKey,QDir::currentPath());
    iniSettingsDict.insert(sqliteDbDir_iniParamKey,QDir::currentPath());

}

void shareElements::updateIniFile(QString key, QString value)
{
    QSettings iniSettings(fullIniFilePath, QSettings::IniFormat);
    iniSettings.setValue(key, value);
    iniSettings.sync();
}

