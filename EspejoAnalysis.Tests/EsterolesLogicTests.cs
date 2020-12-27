using EspejoAnalysis.Model;
using Moq;
using System.Collections.Generic;
using System.IO.Abstractions;
using Xunit;

namespace EspejoAnalysis.Tests
{
    public class EsterolesLogicTests
    {
        private readonly Mock<IFileSystem> _mockFileSystem;

        public EsterolesLogicTests()
        {
            _mockFileSystem = new Mock<IFileSystem>();
        }

        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] 
            { 
                "30592.csv", 
                new string[] {
                    "1	15.2403869628906	21.8901462554932",
                    "2	15.6879329681397	892.425476074219",
                    "3	16.697998046875	2.55308662414551",
                    "4	18.6097412109375	7.48704338073731",
                    "5	18.8832931518555	268.563293457031",
                    "6	19.2593193054199	6.9923849105835",
                    "7	20.0618362426758	89.4883270263672",
                    "8	20.9590320587158	39.138542175293",
                    "9	21.4508323669434	2.20403234958649",
                    "10	21.8433494567871	71.2370300292969",
                    "11	23.1388092041016	7200.38671875",
                    "12	23.2629699707031	60.191104888916",
                    "13	23.4919834136963	310.282592773438",
                    "14	24.5692386627197	22.9326782226563",
                    "15	25.3449745178223	26.1540870666504",
                    "16	26.1611404418946	28.754753112793",
                    "17	31.2715110778809	297.894317626953",
                    "18	33.4217910766602	18.482349395752"
                },
                new EsterolesResult 
                {
                    Brasicasterol = 0.031294515398333889, 
                    Campesterol = 3.2919204711008327, 
                    Colesterol = 0.26831894875231227, 
                    EritrodiolAbsoluto = 66.760603683657777, 
                    EritrodiolPlusUvaol = 3.7332199062199525, 
                    EsterolesAbsoluto = 1828.3332423254358, 
                    Estigmasterol = 1.0969051349893317, 
                    Name = "30592", 
                    ToleranceBrasicasterol = true, 
                    ToleranceCampesterol = true, 
                    ToleranceColesterol = true, 
                    ToleranceEritrodiolPlusUvaol = true, 
                    ToleranceEsterolesAbsoluto = true, 
                    ToleranceEstigmasterol = true, 
                    ToleranceβSitosterol = true, 
                    Toleranceδ7Estigmastenol = true, 
                    UvaolAbsoluto = 4.142048807717984, 
                    βSitosterol = 93.981291163439849, 
                    δ7Estigmastenol = 0.32058429694315305
                }
            }, 

        };

        [Theory]
        [MemberData(nameof(Data))]
        public void CalculateShould(string file, string[] readLines, EsterolesResult expected)
        {
            // Arrange
            _mockFileSystem.Setup(f => f.File.ReadLines(file))
                .Returns(readLines);
            _mockFileSystem.Setup(f => f.Path.GetFileNameWithoutExtension(file))
                .Returns(file.Replace(".csv", ""));

            // Act
            var esterolesLogic = new EsterolesLogic(_mockFileSystem.Object);
            EsterolesResult result = esterolesLogic.Calculate(file);

            // Assert
            Assert.Equal(expected.Brasicasterol, result.Brasicasterol);
            Assert.Equal(expected.Campesterol, result.Campesterol);
            Assert.Equal(expected.Colesterol, result.Colesterol);
            Assert.Equal(expected.EritrodiolAbsoluto, result.EritrodiolAbsoluto);
            Assert.Equal(expected.EritrodiolPlusUvaol, result.EritrodiolPlusUvaol);
            Assert.Equal(expected.EritrodiolPlusUvaolAbs, result.EritrodiolPlusUvaolAbs);
            Assert.Equal(expected.EsterolesAbsoluto, result.EsterolesAbsoluto);
            Assert.Equal(expected.Estigmasterol, result.Estigmasterol);
            Assert.Equal(expected.Name, result.Name);
            Assert.Equal(expected.ToleranceBrasicasterol, result.ToleranceBrasicasterol);
            Assert.Equal(expected.ToleranceCampesterol, result.ToleranceCampesterol);
            Assert.Equal(expected.ToleranceColesterol, result.ToleranceColesterol);
            Assert.Equal(expected.ToleranceEritrodiolPlusUvaol, result.ToleranceEritrodiolPlusUvaol);
            Assert.Equal(expected.ToleranceEsterolesAbsoluto, result.ToleranceEsterolesAbsoluto);
            Assert.Equal(expected.ToleranceEstigmasterol, result.ToleranceEstigmasterol);
            Assert.Equal(expected.ToleranceβSitosterol, result.ToleranceβSitosterol);
            Assert.Equal(expected.Toleranceδ7Estigmastenol, result.Toleranceδ7Estigmastenol);
            Assert.Equal(expected.UvaolAbsoluto, result.UvaolAbsoluto);
            Assert.Equal(expected.βSitosterol, result.βSitosterol);
            Assert.Equal(expected.δ7Estigmastenol, result.δ7Estigmastenol);
        }
    }
}
