#include <QCoreApplication>
#include "SettingsConverter.h"
#include <QPair>

int main(int argc, char *argv[])
{
    QTextCodec::setCodecForLocale(QTextCodec::codecForName("UTF-8"));
    QCoreApplication a(argc, argv);
    QStringList globalTabels;
    globalTabels.append("D:\\My_Folder\\Important\\Programming\\WorkProjects\\taxi\\QtVer2\\Taxi.xlsx");
    QList<QStringList> fpath;
    fpath.append(QStringList());
    fpath.append(QStringList());




    fpath[0].append("D:\\My_Folder\\Important\\Programming\\WorkProjects\\DataMerge_vk\\1\\test.xlsx");



    //fpath[0].append("D:\\My_Folder\\Important\\Programming\\WorkProjects\\DataMerge_vk\\694k_vk.xlsx");

    QList<QPair<QString,QString>> tableSettings;
    tableSettings.append(QPair<QString,QString>("email","varchar(200)"));
    tableSettings.append(QPair<QString,QString>("password","varchar(250)"));
    tableSettings.append(QPair<QString,QString>("isdn","varchar(45)"));
    tableSettings.append(QPair<QString,QString>("full_name","varchar(400)"));
    tableSettings.append(QPair<QString,QString>("id","varchar(20)"));
    tableSettings.append(QPair<QString,QString>("url","varchar(400)"));
    tableSettings.append(QPair<QString,QString>("was_banned","varchar(20)"));
    tableSettings.append(QPair<QString,QString>("date_of_birth","varchar(20)"));


    for (int var = 0; var < globalTabels.length(); ++var) {
        for (QString& processingFile : fpath[var]) {
            SettingsConverter fileDataConverter(processingFile,globalTabels[var],tableSettings);
            if (fileDataConverter.openFile())
            {
                cout << "file was open: " <<processingFile.toStdString()<< endl;
                if (fileDataConverter.processData()) {
                    qDebug()<<"ALL DONE"<<endl;
                }
                else {
                    qDebug()<<"cant read"<<endl;
                    exit(-1);
                }
            }
            else {
                qDebug()<<"cant open"<<endl;
                exit(-1);
            }//argc ==2  // if(cnv.openBook())
        }
    }
    a.exec();
    a.exit(0);


}
