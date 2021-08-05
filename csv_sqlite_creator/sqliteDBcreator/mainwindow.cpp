#include "mainwindow.h"
#include "ui_mainwindow.h"



MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    fd= new QFileDialog();
    fd->setFileMode(QFileDialog::ExistingFile);
    ui->Separator_lineEdit->setMaxLength(1);
    ui->Separator_lineEdit->setText(",");

}

MainWindow::~MainWindow()
{
    delete ui;
    if (saveDbDialog!=nullptr) {delete saveDbDialog;}
    if (fd!=nullptr) {delete  fd;}
    if (shareElements::data_headers!=nullptr) {delete shareElements::data_headers;}
    if (shareElements::dialog_message_box!=nullptr) {delete shareElements::dialog_message_box;}
    if (shareElements::data_container!=nullptr) {
        for (int var = 0; var <shareElements:: data_container->length(); ++var) {
            delete shareElements::data_container->at(var);
        }
        delete shareElements::data_container;
    }
}


void MainWindow::on_OpenCSV_pushButton_clicked()
{
    QString selected_file_name=NULL;
     shareElements::readIniFile();
     fd->setDirectory(shareElements::iniSettingsDict[shareElements::csvDir_iniParamKey]);
    selected_file_name= fd->getOpenFileName(this,"Open csv","","csv file(*.csv)");
    QString folder_path=QFileInfo(selected_file_name).absolutePath();
    shareElements::updateIniFile(shareElements::csvDir_iniParamKey,folder_path);
    if (!selected_file_name.isNull()) {
        separator_charter=ui->Separator_lineEdit->text();
        if (separator_charter!="" and !separator_charter.isEmpty() ) {
            ui->File_path_lineEdit->setText(selected_file_name);
            ui->Save_sqlite_pushButton->setEnabled(true);
             shareElements::data_container= new QList<QStringList*>();
             shareElements::data_headers= new QStringList();
            if (readCSV(selected_file_name,shareElements::data_container,shareElements::data_headers)) {
                printDataToUI(shareElements::data_headers,shareElements::data_container);
            }
            else {
                shareElements::showDialogMsg(QMessageBox::Icon::Critical,"File  read error","Cannot read file");
                shareElements::data_container->clear();
                shareElements::data_headers->clear();
            }
        }
        else {
           shareElements::showDialogMsg(QMessageBox::Icon::Critical,"Separator error","Separator symbol is empty");
        }
    }
    else {
        shareElements::showDialogMsg(QMessageBox::Icon::Critical,"File error","No selected file");
    }
}


bool MainWindow::readCSV(const QString csv_path,  QList<QStringList*>*  container,QStringList*  headers)
{
    QFile f(csv_path);
    if (f.exists() && container!=nullptr && headers!=nullptr) {
        f.open(QIODevice::ReadOnly);
        QTextStream in_stream(&f);
        headers->append(in_stream.readLine().split(separator_charter));
        while (!in_stream.atEnd()) {
            container->append(new QStringList((in_stream.readLine().split(separator_charter))));
        }
        in_stream.flush();
        f.close();
        return true;
    }
    return false;


}

void MainWindow::printDataToUI(QStringList* headers, QList<QStringList*>* data)
{
    QStandardItemModel*  dataModel= new QStandardItemModel();
    dataModel->setHorizontalHeaderLabels(*headers);
    for (int row = 0; row < data->length(); ++row) {
        for (int column = 0; column < data->at(row)->length(); ++column) {
            dataModel->setItem(row,column,new QStandardItem(data->at(row)->at(column)));
        }
    }
    ui->tableView->setModel(dataModel);
}





void MainWindow::on_Save_sqlite_pushButton_clicked()
{
    saveDbDialog = new SaveDbDialog();
    saveDbDialog->show();
}

