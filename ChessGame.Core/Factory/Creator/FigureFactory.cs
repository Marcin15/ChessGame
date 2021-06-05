using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ChessGame.Core
{
    class FigureFactory : IFigureCreator
    {
        public void Create(List<IField> fields)
        {
            IFigureFactory mFactory = null;
            Figure figure = null;
            var initialLocalizations = InitialLocalizationOfFigures.initialLocalizations;

            for (int i = 0; i < initialLocalizations.Length; i++)
            {
                switch (initialLocalizations[i].TypeOfFigure)
                {
                    case "Rook":
                        mFactory = new RookFactory(fields.Where(x => x.RowIndex == initialLocalizations[i].Row && x.ColumnIndex == initialLocalizations[i].Column).FirstOrDefault(), initialLocalizations[i].Player);
                        figure = mFactory.GetFigure();
                        break;
                    case "Knight":
                        mFactory = new KnightFactory(fields.Where(x => x.RowIndex == initialLocalizations[i].Row && x.ColumnIndex == initialLocalizations[i].Column).FirstOrDefault(), initialLocalizations[i].Player);
                        figure = mFactory.GetFigure();
                        break;
                    case "Bishop":
                        mFactory = new BishopFactory(fields.Where(x => x.RowIndex == initialLocalizations[i].Row && x.ColumnIndex == initialLocalizations[i].Column).FirstOrDefault(), initialLocalizations[i].Player);
                        figure = mFactory.GetFigure();
                        break;
                    case "Queen":
                        mFactory = new QueenFactory(fields.Where(x => x.RowIndex == initialLocalizations[i].Row && x.ColumnIndex == initialLocalizations[i].Column).FirstOrDefault(), initialLocalizations[i].Player);
                        figure = mFactory.GetFigure();
                        break;
                    case "King":
                        mFactory = new KingFactory(fields.Where(x => x.RowIndex == initialLocalizations[i].Row && x.ColumnIndex == initialLocalizations[i].Column).FirstOrDefault(), initialLocalizations[i].Player);
                        figure = mFactory.GetFigure();
                        break;
                    case "Pawn":
                        mFactory = new PawnFactory(fields.Where(x => x.RowIndex == initialLocalizations[i].Row && x.ColumnIndex == initialLocalizations[i].Column).FirstOrDefault(), initialLocalizations[i].Player);
                        figure = mFactory.GetFigure();
                        break;
                    default:
                        throw new Exception();
                }
            }
        }
    }
}
