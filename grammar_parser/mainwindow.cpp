#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "QDebug"
QString MainWindow::_keywords= "ADD|ALL|ALTER|ANALYZE|AND|AS|ASC|AUTO_INCREMENT|BDB|"
                               "BERKELEYDB|BETWEEN|BIGINT|BINARY|BLOB|BOTH|BTREE|BY|CASCADE|CASE|CHANGE| CHAR|CHARACTER|CHECK|COLLATE|COLUMN|"
                               "COLUMNS|CONSTRAINT|CREATE|CROSS|CURRENT_DATE|CURRENT_TIME|CURRENT_TIMESTAMP|DATABASE|DATABASES|DAY_HOUR|"
                               "DAY_MINUTE|DAY_SECOND|DEC|DECIMAL|DEFAULT|DELAYED|DELETE|DESC|DESCRIBE|DISTINCT|DISTINCTROW|DIV|DOUBLE|DROP|"
                               "ELSE|ENCLOSED|ERRORS|ESCAPED|EXISTS|EXPLAIN|FALSE|FIELDS|FLOAT|FOR|FORCE|FOREIGN|FROM|FULLTEXT|FUNCTION|GEOMETRY|"
                               "GRANT|GROUP|HASH|HAVING|HELP|HIGH_PRIORITY|HOUR_MINUTE|HOUR_SECOND|IF|IGNORE|IN|INDEX|INFILE|INNER|INNODB|"
                               "INSERT|INT|INTEGER|INTERVAL|INTO|IS|JOIN|KEY|KEYS|KILL|LEADING|LEFT|LIKE|IMIT|LINES|LOAD|LOCALTIME|LOCALTIMESTAMP|"
                               "LOCK|LONG|LONGBLOB|LONGTEXT|LOW_PRIORITY|MASTER_SERVER_ID|MATCH|MEDIUMBLOB|MEDIUMINT|MEDIUMTEXT|MIDDLEINT|"
                               "MINUTE_SECOND|MOD|MRG_MYISAM|NATURAL|NOT|NULL|NUMERIC|ON|OPTIMIZE|OPTION|OPTIONALLY|OR|ORDER|OUTER|OUTFILE|"
                               "PRECISION|PRIMARY|PRIVILEGES|PROCEDURE|PURGE|READ|REAL|REFERENCES|REGEXP|RENAME|REPLACE|REQUIRE|RESTRICT|"
                               "RETURNS|REVOKE|RIGHT|RLIKE|RTREE|SELECT|SET|SHOW|SMALLINT|SOME|SONAME|SPATIAL|SQL_BIG_RESULT|SQL_CALC_FOUND_ROWS|"
                               "SQL_SMALL_RESULT|SSL|STARTING|STRAIGHT_JOIN|STRIPED|TABLE|TABLES|TERMINATED|THEN|TINYBLOB|TINYINT|TINYTEXT|TO|"
                               "TRAILING|TRUE|TYPES|UNION|UNIQUE|UNLOCK|UNSIGNED|UPDATE|USAGE|USE|USER_RESOURCES|USING|VALUES|VARBINARY|VARCHAR|"
                               "VARCHARACTER|VARYING|WARNINGS|WHEN|WHERE|WITH|WRITE|XOR|YEAR_MONTH|ZEROFILL";

QSet<QString> MainWindow::_operators={"<",">","+","-"};
QSet<QString> MainWindow::_result_operators={"=","!"};
QSet<QString> MainWindow::_open_brackets={"(","{","[","<"};
QSet<QString> MainWindow::_closing_brackets={")","}","]",">"};
MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    ui->LexemesTable->horizontalHeader()->setSectionResizeMode(QHeaderView::ResizeToContents  );
    resize( ui->LexemesTable->width(), this->height());
    keywordsSet= new QSet<QString>(_keywords.split("|").toSet());
}
MainWindow::~MainWindow()
{
    delete keywordsSet;
    delete ui;
}
void MainWindow::on_StartBtn_clicked()
{
    QString startStr=ui->QueryInput->text();
    tokenType tokens= LexemesAnalize(startStr);
    QList<QStringList> callStack=SyntaxAnalize(tokens,startStr);
    SetUI(tokens,callStack);
}
LexemType MainWindow::getLexemType(QString token)
{
    bool tokenIsNumber=true;
    for (int t = 0; t <token.length(); ++t) {if (!token.at(t).isDigit()) {tokenIsNumber=false;}}
    if (keywordsSet->contains(token.toUpper())) {return _keyword;}
    if(_operators.contains(token)){return  _operator;}
    if(_result_operators.contains(token)){return  _resultOperator;}
    if(_open_brackets.contains(token)){return  _openBracket;}
    if(_closing_brackets.contains(token)){return  _closingBracket;}
    if (tokenIsNumber){return _number;}
    return _id;
}

void MainWindow::SetUI(tokenType& tokens,QList<QStringList>& callStack)
{
    int maxWidth=0;
    int PreviousMaxWidth=0;
    ui->LexemesTable->clear();
    ui->LexemesTable->setColumnCount(8);
    ui->LexemesTable->setHorizontalHeaderLabels
            (QStringList{"Лекскема","Начало","Длина","Позиция","Тип","Произведение","Стек","Сформированный токен"});
    for (int var = 0; var < ui->LexemesTable->rowCount(); ++var) {
        ui->LexemesTable->removeRow(var);
    }
    for (int var = 0; var < tokens.length()+callStack.length(); ++var) {
        ui->LexemesTable->insertRow(var);
    }
    for (int var = 0; var < tokens.length();++var)
    {
        ui->LexemesTable->setItem(var,0,new QTableWidgetItem(tokens.at(var).first.first));
        for (int i = 0; i < tokens.at(var).second.length(); ++i) {
            ui->LexemesTable->setItem(var,i+1,new QTableWidgetItem(QString::number(tokens.at(var).second.at(i))));
            maxWidth+=  ui->LexemesTable->columnWidth(i);
        }
        for (int d = 1; d < LL_gramar_string.length()-2; ++d)
        {
            checkIf_LL(d);
            if(!correctFlag)
            {
                if (isN) {
                    if (LL_gramar_string.at(incorrectTokenPos+1)!="T") {
                        LL_gramar_string.replace(incorrectTokenPos+1,1,"T");
                    }
                    else if(!(LL_gramar_string.at(incorrectTokenPos+1)=="N"&& LL_gramar_string.at(incorrectTokenPos+2)=="T"))
                    {
                        LL_gramar_string.replace(incorrectTokenPos+1,1,"N");
                        LL_gramar_string.replace(incorrectTokenPos+2,1,"T");
                    }
                }
                else if (isT) {
                    if ( LL_gramar_string.at(incorrectTokenPos-1)!="N")
                    {LL_gramar_string.replace(incorrectTokenPos-1,1,"N");}
                    if ( LL_gramar_string.at(incorrectTokenPos+1)!="N")
                    {LL_gramar_string.replace(incorrectTokenPos+1,1,"N");}
                    if (  LL_gramar_string.at(incorrectTokenPos+1)!="T")
                    {LL_gramar_string.replace(incorrectTokenPos+1,1,"T");}
                }
            }
        }
        Check2(&LL_gramar_string,&vocabuary_LL_gramar_string);
        switch (tokens.at(var).first.second) {
        case 0:
            if (LL_gramar_string.at(var)=="N") {ui->LexemesTable->setItem(var,tokens.at(var).second.length()+1,new QTableWidgetItem("id"));}
            else {ui->LexemesTable->setItem(var,tokens.at(var).second.length()+1,new QTableWidgetItem("keyword"));}
            break;
        case 1:
           {ui->LexemesTable->setItem(var,tokens.at(var).second.length()+1,new QTableWidgetItem("keyword"));}
            break;
        case 2:ui->LexemesTable->setItem(var,tokens.at(var).second.length()+1,new QTableWidgetItem("operator"));
            break;
        case 3:ui->LexemesTable->setItem(var,tokens.at(var).second.length()+1,new QTableWidgetItem("operator"));
            break;
        case 4:ui->LexemesTable->setItem(var,tokens.at(var).second.length()+1,new QTableWidgetItem("operator"));
            break;
        case 5:ui->LexemesTable->setItem(var,tokens.at(var).second.length()+1,new QTableWidgetItem("operator"));
            break;
        case 6:ui->LexemesTable->setItem(var,tokens.at(var).second.length()+1,new QTableWidgetItem("number"));
            break;
        default:ui->LexemesTable->setItem(var,tokens.at(var).second.length()+1,new QTableWidgetItem("not define"));
        }
        if (PreviousMaxWidth<maxWidth) {
            PreviousMaxWidth=maxWidth;
        }

        for (int i = 0; i < callStack.length(); ++i)
        {
            for (int j = callStack.at(i).length()-1; j >=0 ; --j)
            {
                ui->LexemesTable->setItem(i,ui->LexemesTable->columnCount()-1-j,
                                          new QTableWidgetItem(callStack[i][j]));
            }
        }
        resize( PreviousMaxWidth*ui->LexemesTable->columnCount(), this->height());
        ui->LexemesTable->horizontalHeader()->setSectionResizeMode(ui->LexemesTable->columnCount()-1,QHeaderView::Stretch  );
      //  drawDiagram(tokens,callStack);
    }
}
tokenType MainWindow::LexemesAnalize(QString startStr)
{
    tokenType tokens;
    startStr=startStr.simplified();
    QStringList startList= startStr.split("");
    QString tmpToken="";
    int tokenOrder=1;
    int tokenStartPos=startStr.length();
    bool isOperator=false;
    for (int var = startList.length(); var > 1; --var) {
        isOperator=_open_brackets.contains(startList.at(var-1)) || _closing_brackets.contains(startList.at(var-1))
                || _operators.contains(startList.at(var-1))||_result_operators.contains(startList.at(var-1));
        if (startList.at(var-1)!=" " &&
                !(isOperator)) {
            tmpToken+=startList.at(var-1);
        }
        else
        {
pushToken:
            QVector<int> tokenInfo;
            if (!tmpToken.isEmpty()) {
                std::reverse(tmpToken.begin(),tmpToken.end());
                tokenInfo.push_back(tokenStartPos-=tmpToken.length());//token start pos;
                tokenInfo.push_back(tmpToken.length()); //token length
                tokenInfo.push_back(tokenOrder); //tokens order
                tokens.push_back( QPair<QPair<QString,LexemType>, QVector<int>>(QPair<QString,LexemType>
                                                                                (tmpToken,getLexemType(tmpToken)),tokenInfo));
                tokenOrder++;
            }
            tokenInfo.clear();
            tmpToken.clear();
            if (isOperator) {
                tokenInfo.push_back(tokenStartPos-=1);//token start pos;
                tokenInfo.push_back(1); //token length
                tokenInfo.push_back(tokenOrder); //tokens order
                tokens.push_back( QPair<QPair<QString,LexemType>, QVector<int>>(QPair<QString,LexemType>
                                                                                (startList.at(var-1),getLexemType(startList.at(var-1))),tokenInfo));
                tokenOrder++;
            }
        }
    }
    if (!tmpToken.isEmpty()) {
        goto pushToken;
    }
    for (int i = 0; i < tokens.length(); ++i) {
        for (int j = 0; j < tokens.length(); ++j) {
            if (tokens.at(i).second.first()<tokens.at(j).second.first()) {qSwap(tokens[i],tokens[j]);
                qSwap(tokens[i].second.last(),tokens[j].second.last());}
        }
    }
    return  tokens;
}

QList<QStringList> MainWindow::SyntaxAnalize(tokenType &tokens,QString fullStartInputStr)
{
    LL_gramar_string.clear();
    QList<QStringList> callStack; //<token,<stck,production>>
    int StackOrder_stackString=1;
    int _id_ID=0;
    int _operator_ID=0;
    int  _bracket_ID=0;
    int _number_ID=0;
    bool operatorSetFlag=false;
    QString StackTopElement="";
    int StackTopElementID=0;
    callStack.append(QStringList{fullStartInputStr,"<E>",""});
    for (int var = 0; var < tokens.length(); ++var)
    {
        QString constructStackString="";
        QString constructProductionString="";
        switch (tokens.at(var).first.second) {
        case 0://id
            LL_gramar_string+="N";
            vocabuary_LL_gramar_string+="N";
            if (!operatorSetFlag) {constructStackString+=QString("<T"+QString::number(_id_ID)+">");}
            else {
                constructProductionString="<"+StackTopElement+QString::number(StackTopElementID-1)+">";}
            constructProductionString+=constructStackString+"->"+
                    QString("<T"+QString::number(_id_ID)+">")+tokens.at(var).first.first;
            constructProductionString+="->Ɛ";
            operatorSetFlag=false;
            StackTopElement="T";
            StackTopElementID=_id_ID;
            _id_ID++;
            break;
        case 1://keyword
            LL_gramar_string+="N";
            vocabuary_LL_gramar_string+="T";
            StackTopElement="Ɛ-><"+StackTopElement+QString::number(StackTopElementID)+">";
            break;
        case 2://operator
        case 3://_resultOperator
            LL_gramar_string+="T";
            vocabuary_LL_gramar_string+="T";
            operatorSetFlag=true;
            constructProductionString+=constructStackString+"->";
            constructStackString=constructStackString.remove(constructStackString.lastIndexOf("<"),constructStackString.lastIndexOf(">"))+
                    QString("<F"+QString::number(_operator_ID)+">");
            constructProductionString+=constructStackString;
            _operator_ID++;
            StackTopElement="F";
            StackTopElementID=_operator_ID;
            break;
        case 4://_openBracket
            vocabuary_LL_gramar_string+="T";
            LL_gramar_string+="N";
            constructStackString=constructStackString.remove(constructStackString.lastIndexOf("<"),constructStackString.lastIndexOf(">"))+
                    QString("<L"+QString::number( _bracket_ID)+">");
            constructProductionString=constructStackString+"-><K"+QString::number(_bracket_ID)+">";
            _bracket_ID++;
            break;
        case 5://_closingBracket
            LL_gramar_string+="T";
            vocabuary_LL_gramar_string+="T";
            constructStackString=constructStackString.remove(constructStackString.lastIndexOf("<"),constructStackString.lastIndexOf(">"))+
                    QString("<L"+QString::number( _bracket_ID)+">");
            constructProductionString= "<K"+QString::number(_bracket_ID-1)+">"+"->Ɛ";
            _bracket_ID--;
            break;
        case 6://_number
            LL_gramar_string+="N";
            vocabuary_LL_gramar_string+="T";
            constructStackString=constructStackString.remove(constructStackString.lastIndexOf("<"),constructStackString.lastIndexOf(">")+1)+
                    QString("<D"+QString::number( _number_ID)+">"+"->"+tokens.at(var).first.first);
            constructProductionString+=  "<"+StackTopElement+QString::number(StackTopElementID-1)+">"+"->"
                                                                                                      "<D"+QString::number( _number_ID)+">"+"->Ɛ";
            break;
        default:
            break;
        }
        QString tmp=fullStartInputStr;
        if (callStack.last()[StackOrder_stackString]=="") {
            constructStackString=(callStack.last()[StackOrder_stackString])+"->"
                    +callStack.last()[StackOrder_stackString]+"<"+StackTopElement+QString::number(StackTopElementID)+">"+"->Ɛ";
            constructProductionString="Ɛ-> <F"+QString::number(_operator_ID)+">";
            if (_bracket_ID!=0) {
                constructProductionString+="-> <F"+QString::number(_bracket_ID)+">";
            }}
        callStack.append(QStringList{
                             tmp.remove(tokens[var].second[0],tokens[var].second[1]),constructStackString,constructProductionString});
    }
    return  callStack;
}

void MainWindow::drawDiagram(tokenType &tokens, QList<QStringList> &callStack)
{
    static  QMessageBox msg;
    if (!tokens.isEmpty() && !callStack.isEmpty()) {
        d.resize(this->width(),this->height());
        d.setStack(callStack);
        d.setTokens(tokens);
        d.show();
    }
    else
    {
        msg.setText("Множество токенов - пустое. Невозможно отобразить диаграмму.");
        msg.show();
    }
}

void MainWindow::checkIf_LL(int d)
{
    incorrectTokenPos=d;
    if (( LL_gramar_string.at(d)=="T" && LL_gramar_string.at(d-1)=="N" && (LL_gramar_string.at(d+1)=="N" || LL_gramar_string.at(d+1)=="T")))
    {correctFlag=true;
        isT=true;}
    else if((LL_gramar_string.at(d)=="N"&& (LL_gramar_string.at(d+1)=="T" || (LL_gramar_string.at(d+1)=="N"&& LL_gramar_string.at(d+2)=="T"))))
    {correctFlag=true;
    }
    else {correctFlag=false;
        if (LL_gramar_string.at(d)=="N") {
            isN=true;
        }
        else {
            isT=true;
            return;
        }
    }
}
void MainWindow::Check2(QString *LL, QString *LL_voc)
{
    for (int var = 1; var < LL->length(); ++var)
    {
          if ((LL->at(var)==LL_voc->at(var-1)) && LL->at(var)=="T") {
                LL->replace(var,1,"N");
            }

    }
}

