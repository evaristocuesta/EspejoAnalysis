using EspejoAnalysis.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public static IEnumerable<object[]> CalculateShouldReturnExpectedResultsData =>
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
            new object[]
            {
                "6298.csv",
                new string[] {
                    "1	15.0659990310669	13.1270790100098",
                    "2	15.5192441940308	970.975769042969",
                    "3	16.5138854980469	3.46754050254822",
                    "4	18.3964176177979	34.041446685791",
                    "5	18.6881179809571	342.135711669922",
                    "6	19.216444015503	12.1636285781861",
                    "7	19.8169326782227	100.328056335449",
                    "8	20.6587047576905	8.22557353973389",
                    "9	21.5743675231934	41.1905136108399",
                    "10	21.617956161499	26.8627834320069",
                    "11	22.7881736755371	6513.19287109375",
                    "12	22.9324607849121	35.7360000610352",
                    "13	23.2706756591797	958.352172851563",
                    "14	24.2589874267578	72.9895858764649",
                    "15	25.0071105957031	11.2919683456421",
                    "16	25.8271179199219	29.1757678985596",
                    "17	30.7680263519287	144.391708374024",
                    "18	32.9299697875977	14.86256980896"
                },
                new EsterolesResult
                {
                    Brasicasterol = 0.042275321091646979,
                    Campesterol = 4.1712265674001214,
                    Colesterol = 0.16004181630633063,
                    EritrodiolAbsoluto = 29.74156780788508,
                    EritrodiolPlusUvaol = 1.9046057764297713,
                    EsterolesAbsoluto = 1689.4923562461261,
                    Estigmasterol = 1.2231726761273705,
                    Name = "6298",
                    ToleranceBrasicasterol = true,
                    ToleranceCampesterol = false,
                    ToleranceColesterol = true,
                    ToleranceEritrodiolPlusUvaol = true,
                    ToleranceEsterolesAbsoluto = true,
                    ToleranceEstigmasterol = true,
                    ToleranceβSitosterol = true,
                    Toleranceδ7Estigmastenol = true,
                    UvaolAbsoluto = 3.0613678080986508,
                    βSitosterol = 93.2463080347862,
                    δ7Estigmastenol = 0.13766864070309304
                }
            },
            new object[]
            {
                "6300.csv",
                new string[] {
                    "1	15.0525722503662	11.2586860656739",
                    "2	15.5013933181763	841.347778320313",
                    "3	16.5075073242188	2.61390447616577",
                    "4	18.3808403015137	32.7239875793457",
                    "5	18.6574687957764	271.377105712891",
                    "6	19.0504760742188	9.36944389343262",
                    "7	19.799732208252	75.5813140869141",
                    "8	20.6555500030518	9.44005680084229",
                    "9	21.5571060180664	34.6052742004395",
                    "10	21.5896587371826	22.1643333435059",
                    "11	22.7089099884033	5336.40283203125",
                    "12	22.8808403015137	31.4496459960938",
                    "13	23.2241744995117	905.882995605469",
                    "14	24.2234516143799	55.6589698791504",
                    "15	24.9927234649658	7.74906587600708",
                    "16	25.8063144683838	19.693416595459",
                    "17	30.7538661956787	105.053855895996",
                    "18	32.9319648742676	4.70972013473511"
                },
                new EsterolesResult
                {
                    Brasicasterol = 0.03829351844385543,
                    Campesterol = 3.9756556896448925,
                    Colesterol = 0.16493896637794334,
                    EritrodiolAbsoluto = 24.972754098365392,
                    EritrodiolPlusUvaol = 1.582580393162405,
                    EsterolesAbsoluto = 1622.6276952368435,
                    Estigmasterol = 1.1072609850087438,
                    Name = "6300",
                    ToleranceBrasicasterol = true,
                    ToleranceCampesterol = true,
                    ToleranceColesterol = true,
                    ToleranceEritrodiolPlusUvaol = true,
                    ToleranceEsterolesAbsoluto = true,
                    ToleranceEstigmasterol = true,
                    ToleranceβSitosterol = true,
                    Toleranceδ7Estigmastenol = true,
                    UvaolAbsoluto = 1.1195655960815079,
                    βSitosterol = 93.556858372006928,
                    δ7Estigmastenol = 0.11352327514309252
                }
            }
        };

        [Theory]
        [MemberData(nameof(CalculateShouldReturnExpectedResultsData))]
        public void CalculateShouldReturnExpectedResults(string file, string[] readLines, EsterolesResult expected)
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

        [Theory]
        [InlineData("file17.csv", new string[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" })]
        [InlineData("file19.csv", new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" })]
        public void CalculateShouldThrowExceptionWhenFileIncorrectFormat(string file, string [] readLines)
        {
            // Arrange
            _mockFileSystem.Setup(f => f.File.ReadLines(file))
                .Returns(readLines);

            // Act
            var esterolesLogic = new EsterolesLogic(_mockFileSystem.Object);

            // Assert
            var ex = Assert.Throws<Exception>(() => esterolesLogic.Calculate(file));
            Assert.Equal($"El archivo {file} no tiene el formato correcto\n", ex.Message);
        }

        [Theory]
        [InlineData("file18.csv", new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" })]
        [InlineData("file18.csv", new string[] { "1\taaaa\tbbbb", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" })]
        public void CalculateShouldThrowExceptionWhenCannotReadNumbersInFile(string file, string[] readLines)
        {
            // Arrange
            _mockFileSystem.Setup(f => f.File.ReadLines(file))
                .Returns(readLines);

            // Act
            var esterolesLogic = new EsterolesLogic(_mockFileSystem.Object);

            // Assert
            var ex = Assert.Throws<Exception>(() => esterolesLogic.Calculate(file));
            Assert.Equal($"El archivo {file} no tiene el formato correcto\n", ex.Message);
        }

        public static IEnumerable<object[]> ExportShouldExportCSVData =>
        new List<object[]>
        {
            new object[]
            {
                "export1.csv",
                new ObservableCollection<EsterolesResult>
                {
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
                    },
                    new EsterolesResult
                    {
                        Brasicasterol = 0.042275321091646979,
                        Campesterol = 4.1712265674001214,
                        Colesterol = 0.16004181630633063,
                        EritrodiolAbsoluto = 29.74156780788508,
                        EritrodiolPlusUvaol = 1.9046057764297713,
                        EsterolesAbsoluto = 1689.4923562461261,
                        Estigmasterol = 1.2231726761273705,
                        Name = "6298",
                        ToleranceBrasicasterol = true,
                        ToleranceCampesterol = false,
                        ToleranceColesterol = true,
                        ToleranceEritrodiolPlusUvaol = true,
                        ToleranceEsterolesAbsoluto = true,
                        ToleranceEstigmasterol = true,
                        ToleranceβSitosterol = true,
                        Toleranceδ7Estigmastenol = true,
                        UvaolAbsoluto = 3.0613678080986508,
                        βSitosterol = 93.2463080347862,
                        δ7Estigmastenol = 0.13766864070309304
                    },
                    new EsterolesResult
                    {
                        Brasicasterol = 0.03829351844385543,
                        Campesterol = 3.9756556896448925,
                        Colesterol = 0.16493896637794334,
                        EritrodiolAbsoluto = 24.972754098365392,
                        EritrodiolPlusUvaol = 1.582580393162405,
                        EsterolesAbsoluto = 1622.6276952368435,
                        Estigmasterol = 1.1072609850087438,
                        Name = "6300",
                        ToleranceBrasicasterol = true,
                        ToleranceCampesterol = true,
                        ToleranceColesterol = true,
                        ToleranceEritrodiolPlusUvaol = true,
                        ToleranceEsterolesAbsoluto = true,
                        ToleranceEstigmasterol = true,
                        ToleranceβSitosterol = true,
                        Toleranceδ7Estigmastenol = true,
                        UvaolAbsoluto = 1.1195655960815079,
                        βSitosterol = 93.556858372006928,
                        δ7Estigmastenol = 0.11352327514309252
                    }
                },
                new List<string>
                {
                    "Analisis;EC;EB;ECA;EES;EBS;ED7E;ESTA;ERIABS;UVAABS;ERIUVAABS;ERI",
                    "30592;0,268318948752312;0,0312945153983339;3,29192047110083;1,09690513498933;93,9812911634398;0,320584296943153;1828,33324232544;66,7606036836578;4,14204880771798;70,9026524913758;3,73321990621995",
                    "6298;0,160041816306331;0,042275321091647;4,17122656740012;1,22317267612737;93,2463080347862;0,137668640703093;1689,49235624613;29,7415678078851;3,06136780809865;32,8029356159837;1,90460577642977",
                    "6300;0,164938966377943;0,0382935184438554;3,97565568964489;1,10726098500874;93,5568583720069;0,113523275143093;1622,62769523684;24,9727540983654;1,11956559608151;26,0923196944469;1,5825803931624"
                }
            }
        };

        [Theory]
        [MemberData(nameof(ExportShouldExportCSVData))]
        public void ExportShouldExportCSV(string path, ObservableCollection<EsterolesResult> results, List<string> expected)
        {
            // Arrange
            _mockFileSystem.Setup(f => f.File.WriteAllLines(path, It.IsAny<List<string>>()));

            // Act
            var esterolesLogic = new EsterolesLogic(_mockFileSystem.Object);
            List<string> stringsToCsv = esterolesLogic.ConvertResultToStringsToCsv(results);

            // Assert
            Assert.Equal(expected.Count, stringsToCsv.Count);
            Assert.Equal(expected[0], stringsToCsv[0]);
            Assert.Equal(expected[1], stringsToCsv[1]);
            Assert.Equal(expected[2], stringsToCsv[2]);
            Assert.Equal(expected[3], stringsToCsv[3]);
        }
    }
}
