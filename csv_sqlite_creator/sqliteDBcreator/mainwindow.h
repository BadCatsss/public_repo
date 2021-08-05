#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QFile>
#include <QStandardItemModel>
#include "savedbdialog.h"

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:
    void on_OpenCSV_pushButton_clicked();
    void on_Save_sqlite_pushButton_clicked();

private:
    Ui::MainWindow *ui;
    SaveDbDialog* saveDbDialog;
    QFileDialog* fd;
    QString separator_charter;


    bool readCSV(const QString csv_path, QList<QStringList*>*,QStringList* headers);
    void printDataToUI( QStringList* headers, QList<QStringList*>* data_container);

};
#endif // MAINWINDOW_H
