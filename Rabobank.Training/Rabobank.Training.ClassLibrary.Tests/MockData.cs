using Rabobank.Training.ClassLibrary.ViewModels;

namespace Rabobank.Training.ClassLibrary.Tests
{
    public static class MockData
    {
        public static PositionVM GetMockPositionVM()
        {
            return new PositionVM
            {
                Code = "Pos1",
                Name = "Position1",
                Value = 12345,
                Mandates = null
            };
        }

        public static List<MandateVM> GetMockMandateVMList()
        {
            return new List<MandateVM>
                     {
                         new MandateVM { Allocation=.105m, Name="Mandate1", Value = 1296},
                         new MandateVM { Allocation=.205m, Name="Mandate2", Value=2531 },
                         new MandateVM { Allocation=.305m, Name= "Mandate3", Value=3765},
                         new MandateVM { Allocation=.109m, Name="Mandate4", Value=1346 },
                          new MandateVM { Allocation=.025m, Name="Liquidity", Value=309 }
                     };
        }

        public static FundOfMandates GetMockFundOfMandates()
        {
            return new FundOfMandates
            {

                InstrumentCode = "Pos1",
                InstrumentName = "FundOfMandates1",
                LiquidityAllocation = 2.5m,
                Mandates = new Mandate[]
                     {
                         new Mandate { Allocation=10.5m, MandateId="Mandate1", MandateName = "Mandate1" },
                         new Mandate { Allocation=20.5m, MandateId="Mandate2", MandateName = "Mandate2" },
                         new Mandate { Allocation=30.5m, MandateId="Mandate3", MandateName = "Mandate3" },
                         new Mandate { Allocation=10.9m, MandateId="Mandate4", MandateName = "Mandate4" }
                     }

            };
        }

        public static PortfolioVM GetMockPortfolioVMList()
        {
            return new PortfolioVM
            {
                Positions = new List<PositionVM> {

                     new PositionVM { Code="NL0000009165", Name="Heineken", Value=12345 },
                     new PositionVM { Code="NL0000287100", Name="Optimix Mix Fund", Value=23456 },
                     new PositionVM { Code="LU0035601805", Name="DP Global Strategy L High", Value=34567 },
                     new PositionVM { Code="NL0000292332", Name="Rabobank Core Aandelen Fonds T2", Value=45678 },
                     new PositionVM { Code="LU0042381250", Name="Morgan Stanley Invest US Gr Fnd", Value=56789 }
                    }
            };
        }
    }
}
