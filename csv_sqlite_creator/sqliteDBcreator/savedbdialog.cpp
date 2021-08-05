#include "savedbdialog.h"
#include "ui_savedbdialog.h"

SaveDbDialog::SaveDbDialog(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::SaveDbDialog)
{
    ui->setupUi(this);
}

SaveDbDialog::~SaveDbDialog()
{
    delete ui;
    delete selectFolderDialog;
}

void SaveDbDialog::on_select_directory_toolButton_clicked()
{
    shareElements::readIniFile();

    selectFolderDialog= new QFileDialog();
    selectFolderDialog->setDirectory(shareElements::iniSettingsDict[shareElements::sqliteDbDir_iniParamKey]);
    selectFolderDialog->setFileMode(QFileDialog::FileMode::Directory);
    selectedFolderForSave= selectFolderDialog->getExistingDirectory();
    shareElements::updateIniFile(shareElements::sqliteDbDir_iniParamKey,selectedFolderForSave);
    ui->save_directory_lineEdit->setText(selectedFolderForSave);
    shareElements::readIniFile();
}


void SaveDbDialog::on_ok_pushButton_clicked()
{
    dbName=ui->db_file_name_lineEdit->text();
    if (!selectedFolderForSave.isNull() && !selectedFolderForSave.isEmpty() && !dbName.isNull() && !dbName.isEmpty()) {
        foreach (QString unallowed_symbol , invalidCharacters) {
            dbName.remove(unallowed_symbol);
        }
        if (dbName.length()>0) {
            createSqliteDbFile(dbName,shareElements::data_headers);
            writeToDb(dbName,shareElements::data_headers,shareElements::data_container);
        }
        else {
            shareElements::showDialogMsg(QMessageBox::Icon::Critical,"Db create error","File name contains unallowed symbol. Name can contain only letters and numbers.");
        }
    }
    else {
        shareElements::showDialogMsg(QMessageBox::Icon::Critical,"Db create error","File name or directory path is empty");
    }
}

void SaveDbDialog::on_cancel_pushButton_clicked()
{
    this->close();
}

void SaveDbDialog::createSqliteDbFile(QString tableName, QStringList* headers)
{
    db = QSqlDatabase::addDatabase("QSQLITE");
    db.setDatabaseName(selectedFolderForSave+shareElements::getSlash()+dbName+".db");
    db.open();
    QSqlQuery createQuery;
    QString createQueryString="CREATE TABLE IF NOT EXISTS '"+tableName+"' ( id INTEGER PRIMARY KEY NOT NULL,";
    for (QString header : *headers) {
        createQueryString+="'"+header+"'"+" TEXT NOT NULL,";
    }
    createQueryString.remove(createQueryString.length()-1,1);
    createQueryString+=");";
    if (createQuery.prepare(createQueryString)) {
        createQuery.exec();
    }
    else {
         shareElements::showDialogMsg(QMessageBox::Icon::Critical,"Create table error", createQuery.lastError().text());
    }
}

void SaveDbDialog::writeToDb(QString tableName ,QStringList* headers, QList<QStringList*>* data_container)
{
    if (db.tables().contains( QString(tableName))) {
        QSqlQuery insertQuery;
        QString insertQueryString="INSERT INTO '"+tableName+"' (";
        for (QString header : *headers) {
            insertQueryString+="'"+header+"'"+",";
        }
        insertQueryString.remove(insertQueryString.length()-1,1);
        insertQueryString+=") VALUES (";
        QString BaseinsertQueryString=insertQueryString;
       int insertedRowsCount=0;
        for (auto var : *data_container) {
            for (auto var1 : *var) {
                insertQueryString+="'"+var1+"'"+",";
            }
            insertQueryString.remove(insertQueryString.length()-1,1);
            insertQueryString+=");";
            if (insertQuery.prepare(insertQueryString)) {
                insertQuery.exec(insertQueryString);
                insertedRowsCount++;
            }
            else{
                  shareElements::showDialogMsg(QMessageBox::Icon::Critical,"Row insert error", "Error:"+insertQuery.lastError().text()+".It is not possible to insert the "+QString::number(insertedRowsCount+1)+"th record. Click 'OK' for skip and continue.");
            }
            insertQueryString=BaseinsertQueryString;
        }
        shareElements::showDialogMsg(QMessageBox::Icon::Information,"Success","The database file has been created. "+QString::number(insertedRowsCount)+" records have been added to the database.");
    }
}

