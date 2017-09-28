using System.Collections.Generic;
using Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using DataLayer;

namespace ParsingUtility
{
    public class PdfCreator : IDataExporter
    {
        static BaseFont helveticaFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED);
        static Font normalFont = new Font(helveticaFont, 12);
        static Font bigFont = new Font(helveticaFont, 16);
        static Font titleFont = new Font(helveticaFont, 20, Font.BOLD);
        static Font smallFont = new Font(helveticaFont, 10);

        public void ExportData(string fileName, StatisticsData statisticsData)
        {
            Document doc = new Document(PageSize.A4);
            FileStream output = new FileStream(fileName, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            //Open document to write
            doc.Open();

            //Adding paragraphs to document
            doc.Add(CreateTitleParagraph());
            doc.Add(CreateVotesPerCandidateParagraph(statisticsData));
            doc.Add(CreateVotesPerPartyParagraph(statisticsData));
            doc.Add(CreateOtherStatisticsParagraph(statisticsData));

            //Closing document
            doc.Close();
        }

        Paragraph CreateTitleParagraph()
        {
            Paragraph titleParagraph = new Paragraph("VOTING RESULTS", titleFont);
            return titleParagraph;
        }

        Paragraph CreateVotesPerCandidateParagraph(StatisticsData statisticsData)
        {
            Phrase titlePhrase = new Phrase("\n\nVOTES PER CANDIDATE\n\n", bigFont);
            Paragraph votesPerCandidateParagraph = new Paragraph();
            votesPerCandidateParagraph.Add(titlePhrase);

            //ExportData exportData = new ExportData();
            foreach (KeyValuePair<Candidate, int> candidateVotes in statisticsData.candidateVotes)
            {
                Chunk nameChunk = new Chunk(candidateVotes.Key.name + "   ", normalFont);
                float percentVotingResult = candidateVotes.Value / (float)statisticsData.validVotesNumber * 100;
                Chunk resultChunk = new Chunk(string.Format("{0} ({1:0.00}%)\n", candidateVotes.Value, percentVotingResult), normalFont);
                Chunk partyChunk = new Chunk(candidateVotes.Key.party + "\n\n", smallFont);

                Phrase candidatePhrase = new Phrase();
                candidatePhrase.Add(nameChunk);
                candidatePhrase.Add(resultChunk);
                candidatePhrase.Add(partyChunk);

                votesPerCandidateParagraph.Add(candidatePhrase);
            }
            return votesPerCandidateParagraph;
        }

        Paragraph CreateVotesPerPartyParagraph(StatisticsData statisticsData)
        {
            Phrase titlePhrase = new Phrase("\nVOTES PER PARTY\n\n", bigFont);
            Paragraph votesPerPartyParagraph = new Paragraph();
            votesPerPartyParagraph.Add(titlePhrase);

            foreach (KeyValuePair<string, int> partyVotes in statisticsData.partyVotes)
            {
                Chunk partyChunk = new Chunk(partyVotes.Key + "    ", normalFont);
                float percentVotingResult = partyVotes.Value / (float)statisticsData.validVotesNumber * 100;
                Chunk resultChunk = new Chunk(string.Format("{0} ({1:0.00}%)\n\n", partyVotes.Value, percentVotingResult), normalFont);
                Phrase partyPhrase = new Phrase();
                partyPhrase.Add(partyChunk);
                partyPhrase.Add(resultChunk);
                votesPerPartyParagraph.Add(partyPhrase);
            }
            return votesPerPartyParagraph;
        }

        Paragraph CreateOtherStatisticsParagraph(StatisticsData statisticsData)
        {
            Phrase titlePhrase = new Phrase("\nOTHER STATISTICS\n\n", bigFont);
            Paragraph otherStatisticsParagraph = new Paragraph();
            otherStatisticsParagraph.Add(titlePhrase);
            float percentVotingResult = statisticsData.invalidVotesNumber / (float)(statisticsData.validVotesNumber + statisticsData.invalidVotesNumber) * 100;
            Phrase invalidVotes = new Phrase(string.Format("Number of invalid votes {0} ({1:0.00}%)\n\n", statisticsData.invalidVotesNumber, percentVotingResult), normalFont);
            otherStatisticsParagraph.Add(invalidVotes);
            Phrase withoutRightsVotes = new Phrase("Number of voting attemps by person deprived of its voting rights: " + statisticsData.withoutRightsVotesNumber + "\n\n", normalFont);
            otherStatisticsParagraph.Add(withoutRightsVotes);

            return otherStatisticsParagraph;
        }
    }
}
