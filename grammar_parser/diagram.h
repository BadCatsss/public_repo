#ifndef DIAGRAM_H
#define DIAGRAM_H

#include <QWidget>
#include "types.h"
enum pivotPoint
{
    Top,
    Bottom,
    Left,
    Right,
    Center
};
enum shapeType
{
    Rectangle,
    Cyrcle,
    Arrow,
};

class diagram :public QWidget
{
    tokenType t;
    QList<QStringList> stack;
public:
    diagram();
  void  setStack( QList<QStringList> stack);
  void setTokens(tokenType tokens);
protected:
    void paintEvent(QPaintEvent *event);
    void drawDiagram(QPainter *qp,tokenType tokens, QList<QStringList> callStack);
    void DrawShape(QPainter *qp,int x, int y, int width, int heigth,shapeType shape,pivotPoint p,QString text);
};

#endif // DIAGRAM_H
