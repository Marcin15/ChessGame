using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public class AttackMechanicContainer : IAttackMechanicContainer
    {
        private readonly IFlagCleaner mFlagCleaner;

        private readonly IPawnsAttackMechanics mPawnsAttackMechanics;
        private readonly IRookAttackMechanics mRookAttackMechanics;
        private readonly IKnightAttackMechanics mKnightAttackMechanics;
        private readonly IBishopAttackMechanics mBishopAttackMechanics;
        private readonly IQueensAttackMechanics mQueensAttackMechanics;
        private readonly IKingsAttacksMechanics mKingsAttacksMechanics;
        public AttackMechanicContainer(IFlagCleaner flagCleaner, 
            IPawnsAttackMechanics pawnsAttackMechanics,
            IRookAttackMechanics rookAttackMechanics, 
            IKnightAttackMechanics knightAttackMechanics, 
            IBishopAttackMechanics bishopAttackMechanics, 
            IQueensAttackMechanics queensAttackMechanics, 
            IKingsAttacksMechanics kingsAttacksMechanics)
        {
            this.mFlagCleaner = flagCleaner;

            this.mPawnsAttackMechanics = pawnsAttackMechanics;
            this.mRookAttackMechanics = rookAttackMechanics;
            this.mKnightAttackMechanics = knightAttackMechanics;
            this.mBishopAttackMechanics = bishopAttackMechanics;
            this.mQueensAttackMechanics = queensAttackMechanics;
            this.mKingsAttacksMechanics = kingsAttacksMechanics;
        }
        public void Container(ObservableCollection<IField> fieldsList)
        {
            mFlagCleaner.Clean(fieldsList);

            mPawnsAttackMechanics.GetPawnsAttackMechanics(fieldsList);
            mRookAttackMechanics.GetRooksAttackMechanics(fieldsList);
            mKnightAttackMechanics.GetKnightsAttackMechanics(fieldsList);
            mBishopAttackMechanics.GetBishopsAttackMechanics(fieldsList);
            mQueensAttackMechanics.GetQueensAttackMechanics(fieldsList);
            mKingsAttacksMechanics.GetKingsAttackMechanics(fieldsList);
        }
    }
}
