#ifndef TYPES_H
#define TYPES_H
#include <QSet>
#include<QVector>
#include <QPair>

#endif // TYPES_H
enum LexemType
{
    _id,
    _keyword,
    _operator,
    _resultOperator,
    _openBracket,
    _closingBracket,
    _number,
    _none
};
typedef QVector<QPair<QPair<QString,LexemType>, QVector<int>>> tokenType ;
