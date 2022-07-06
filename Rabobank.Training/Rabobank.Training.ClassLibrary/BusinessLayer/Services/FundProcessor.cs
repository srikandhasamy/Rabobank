using System.Xml.Serialization;
using Rabobank.Training.ClassLibrary.ViewModels;


namespace Rabobank.Training.ClassLibrary.BusinessLayer
{
    public class FundProcessor : IFundsProcessor
    {
        /// <summary>
        /// Calculate Mandate with Position object And Fundofmandate Objects And Return Updated Positionvm Back To Client.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="fundOfmandates"></param>
        /// <returns></returns>
        public PositionVM GetCalculatedMandates(PositionVM position, FundOfMandates fundOfmandates)
        {

            if (position.Code == fundOfmandates.InstrumentCode && fundOfmandates.Mandates != null && fundOfmandates.Mandates.Length > 0)
            {
                position.Mandates = new List<MandateVM>();
                position.Mandates.AddRange
                 (
                            fundOfmandates.Mandates.Select(x => new MandateVM
                            {
                                Name = x.MandateName,
                                Value = Math.Round((position.Value * x.Allocation) / 100),
                                Allocation = x.Allocation / 100
                            })
                 );

                if (fundOfmandates.LiquidityAllocation > 0)
                {
                    var newMandate = new MandateVM
                    {
                        Name = "Liquidity",
                        Value = Math.Round((position.Value * fundOfmandates.LiquidityAllocation) / 100),
                        Allocation = fundOfmandates.LiquidityAllocation / 100
                    };

                    position.Mandates.Add(newMandate);
                }
            }


            return position;

        }

        /// <summary>
        /// Get Portfolio staticobject and return to Client.
        /// </summary>
        /// <returns></returns>
        public PortfolioVM GetPortfolio()
        {
            var portfolio = new PortfolioVM
            {
                Positions = new List<PositionVM> {

                     new PositionVM { Code="NL0000009165", Name="Heineken", Value=12345 },
                     new PositionVM { Code="NL0000287100", Name="Optimix Mix Fund", Value=23456 },
                     new PositionVM { Code="LU0035601805", Name="DP Global Strategy L High", Value=34567 },
                     new PositionVM { Code="NL0000292332", Name="Rabobank Core Aandelen Fonds T2", Value=45678 },
                     new PositionVM { Code="LU0042381250", Name="Morgan Stanley Invest US Gr Fnd", Value=56789 }
                    }
            }; 

            return portfolio;
        }

        /// <summary>
        /// Read Funds XML file and set Domain Classes accordingly.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<FundOfMandates> ReadFundOfMandatesFile(string fileName)
        {
            try
            {
                List<FundOfMandates>? funds = null;
                StreamReader? reader = null;
                FundsOfMandatesData? fundsOfMandatesData = null;
                var serealizer = new XmlSerializer(typeof(FundsOfMandatesData));

                using (reader = new StreamReader(fileName))
                {
                    fundsOfMandatesData = serealizer.Deserialize(reader) as FundsOfMandatesData;
                } 
                if (fundsOfMandatesData == null)
                {
                    throw new ArgumentException("UnExpected Error. Please check that file contains data correctly.");

                }
                else if (fundsOfMandatesData != null && fundsOfMandatesData!.FundsOfMandates!.Length == 0)
                {
                    throw new ArgumentException("Unable to Read blank FundOfMandatesFile. Please check the file.");
                }
                else
                {
                    funds = fundsOfMandatesData!.FundsOfMandates!.ToList();
                }

                return funds;
            }
            catch (InvalidOperationException inv)
            {
                throw new InvalidOperationException("Invalid Operation Error", inv); 
            }
            catch (Exception ex)
            {
                throw new ArgumentException( ex.Message);
            }
        }

    }
}
