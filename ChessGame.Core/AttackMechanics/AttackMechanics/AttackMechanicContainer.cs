using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChessGame.Core
{
    public class AttackMechanicContainer
    {
        public AttackMechanicContainer(ObservableCollection<IField> fieldsList)
        {
            FlagCleaner flagCleaner = new(fieldsList);

            QueensAttackMechanics queensAttackMechanics = new(fieldsList);
            RookAttackMechanics rookAttackMechanics = new(fieldsList);
            BishopAttackMechanics bishopAttackMechanics = new(fieldsList);
            KnightAttackMechanics knightAttackMechanics = new(fieldsList);
            KingsAttacksMechanics kingsAttacksMechanics = new(fieldsList);
            PawnsAttackMechanics pawnsAttackMechanics = new(fieldsList);
        }
    }
}
