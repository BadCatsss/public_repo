#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include "diagram.h"
#include <QMessageBox>

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

//typedef QList<QPair<QString, QPair<QString,QString>>> callStackType;
class MainWindow : public QMainWindow
{
    Q_OBJECT
    diagram d;
    static   QString _keywords;
    static QSet<QString> _operators;
    static QSet<QString> _result_operators;
    static QSet<QString> _open_brackets;
    static QSet<QString> _closing_brackets;
    QString LL_gramar_string;
     QString vocabuary_LL_gramar_string;
    QSet<QString>* keywordsSet;
    LexemType getLexemType(QString t);
    void SetUI(tokenType& tokens,QList<QStringList>& callStack);
    tokenType LexemesAnalize(QString startStr);
    QList<QStringList> SyntaxAnalize(tokenType& tokens,QString fullStartInputStr);
    void drawDiagram(tokenType& tokens,QList<QStringList>& callStack);
    // **LL type auto-cottect
    bool correctFlag=true;
    int incorrectTokenPos=0;
    bool isT=false;
    bool isN=false;
    void checkIf_LL(int d);
    void Check2(QString* LL, QString* LL_voc);
    // LL type auto-cottect**
public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:
    void on_StartBtn_clicked();

private:
    Ui::MainWindow *ui;
};
#endif // MAINWINDOW_H
