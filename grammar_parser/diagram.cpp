#include "diagram.h"
#include <QPainter>

diagram::diagram()
{}
void diagram::setStack(QList<QStringList> stack)
{this->stack=stack;}
void diagram::setTokens(tokenType tokens)
{    this->t=tokens;}
void diagram::paintEvent(QPaintEvent *event)
{
    Q_UNUSED(event)
    QPainter qp(this);
    drawDiagram(&qp,t,stack);
}
void diagram::DrawShape(QPainter *qp,int x, int y, int width, int heigth,shapeType shape, pivotPoint p, QString text)
{
    static int previousWidth=0;
    static int previousHeigth=0;
    static int previousXcord=0;
    static int previousYcord=0;
    static int textX=x+width/2;
    static int textY=y+heigth/2;
    int xDirection=0;
    int yDirection=0;
    if (x==0) {x=(previousXcord-previousWidth);}
    if (y==0) {y=previousYcord+previousHeigth*2;} // new tree deep level
    if (y==-1) {y=previousYcord-previousHeigth;} // the same tree deep level
    if (y<-1)
    {   for (int var = y; var < -1; ++var)
        {y=-previousYcord-previousHeigth;}
    }
    if (x<-1)
    {   for (int var = x; var < -1; ++var)
        {x+=previousXcord;}
    }
    if (x==-1) {x=previousXcord;}
    switch (p) {
    case Top:
        y-=previousHeigth;
        yDirection-=previousHeigth*2;
        break;
    case Bottom:
        y+=previousHeigth;
        yDirection+=previousHeigth*2;
        x=x-(previousWidth*2);
        break;
    case Left:
        xDirection=previousWidth*-1;
        x-=previousWidth;
        break;
    case Right:
        xDirection=previousWidth;
        x+=previousWidth;
        break;
    case Center:
        x=x-(previousWidth/2);
        y+=previousHeigth/2;
        break;
    }
    textX=x+width/2;
    textY=y+heigth/2;
    switch (shape) {
    case Rectangle:
        qp->drawRect(x+previousWidth/3,y,width,heigth);break;
    case Arrow:
        qp->drawLine(x,y-yDirection*2,x+xDirection,y+yDirection);break;
    case Cyrcle:
        qp->drawEllipse(x+previousWidth,y,width,width);textX+=previousWidth; break;
    }
    previousWidth=width;
    previousHeigth=heigth;
    previousXcord=x;
    previousYcord=y;
    if (text!="" && text!=" ") {
        qp->drawText(textX,textY,text);
    }
}
void diagram::drawDiagram(QPainter* qp,QVector<QPair<QPair<QString,LexemType>, QVector<int>>> tokens, QList<QStringList> callStack)
{
    QPen pen(Qt::black, 2, Qt::SolidLine);
    qp->setPen(pen);
    int shapesCount=0;
    for(auto var: callStack)
    {shapesCount+= var.last().split("->").length();}
    //scale setup
    int Xscale=(this->width()/shapesCount/callStack.length())+1;
    int Yscale=(this->height()/shapesCount/callStack.length())+1;
    if (tokens.length()<10) {
        Yscale/=(((this->height()/shapesCount/callStack.length()))*2);
        Xscale/=((this->width()/shapesCount/callStack.length())*2);
        Yscale+=1;
        Xscale+=1;
    }
    else
    {
        Yscale=(this->height()/shapesCount/callStack.length())+1;
        Xscale=(this->width()/shapesCount/callStack.length())+1;
    }//end scale setup
    DrawShape( qp,this->width()/2,height()/4,100/Xscale,50/Yscale,shapeType::Rectangle,pivotPoint::Center,"S"); //start
    for(auto var: callStack)
    {
        QStringList parts = var.last().split("->");
        for(int i=0;i<parts.length();i++)
        {  //x=0 -> previous x + previousWidth
            //y=0->previoys y + previousHeigth // new  tree deep level
            //y=-1->previoys y // the same  tree deep level
            if (parts[i].contains("T")) {
                if (i<parts.length()-1) {DrawShape( qp,-1,0,-50/Xscale,50/Yscale,shapeType::Arrow,pivotPoint::Right,"");}
                DrawShape( qp,-1,0,-20/Xscale,-10/Yscale*2,shapeType::Arrow,pivotPoint::Top,"");
                DrawShape( qp,-1,0,100/Xscale,50/Yscale,shapeType::Rectangle,pivotPoint::Top,parts[i]);
            }
            if (parts[i].contains("F")||parts[i].contains("L"))
            {DrawShape( qp,-1,-2,100/Xscale,50/Yscale,shapeType::Rectangle,pivotPoint::Bottom,parts[i]);
            }
            if (parts[i].contains("Ɛ")||parts[i].contains("D"))
            {DrawShape( qp,0,-1,50/Xscale,50/Yscale,shapeType::Cyrcle,pivotPoint::Right,parts[i]);}
        }
    }
}
