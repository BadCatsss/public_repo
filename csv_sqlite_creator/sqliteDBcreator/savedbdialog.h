#ifndef SAVEDBDIALOG_H
#define SAVEDBDIALOG_H

#include <QWidget>
#include <QSqlError>
#include "shareelements.h"


namespace Ui {
class SaveDbDialog;
}

class SaveDbDialog : public QWidget
{
    Q_OBJECT

public:
    explicit SaveDbDialog(QWidget *parent = nullptr);
    ~SaveDbDialog();

private slots:
    void on_select_directory_toolButton_clicked();

    void on_ok_pushButton_clicked();

    void on_cancel_pushButton_clicked();

private:
    Ui::SaveDbDialog *ui;
    QFileDialog* selectFolderDialog;
    QString dbName=NULL;
    QString selectedFolderForSave=NULL;
    void createSqliteDbFile(QString tableName, QStringList* headers);
    void writeToDb(QString tableName,QStringList* headers, QList<QStringList*>* data_container);
    QSqlDatabase db;
    const QStringList invalidCharacters={ "~", "@", "#", "$", "%", "^", "-", "_", "(", ")", "{", "}", "`", "+", "=", "[", "]", ":",
                                                       ",", ";",",", ".", "/", "?","/","\\",":","*","?","«","»","<",">","|","&","—","\""," ","'"};




     void showErrorMsg(const QString windowTitle,const QString errorMsg);
};

#endif // SAVEDBDIALOG_H
