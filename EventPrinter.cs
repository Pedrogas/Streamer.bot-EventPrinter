using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

public class CPHInline
{
    private static string SUB_IMAGE_PATH = @"C:\img\sub.png";
    private static string GIFT_IMAGE_PATH = @"C:\img\sub-gift.png";
    private static string PRIME_IMAGE_PATH = @"C:\img\sub-prime.png";
    private static string CHEER_IMAGE_PATH = @"C:\img\cheer100.png";
    private static string CHEER1_IMAGE_PATH = @"C:\img\img\cheer1k.png";
    private static string CHEER5_IMAGE_PATH = @"C:\img\cheer5k.png";
    private static string CHEER10_IMAGE_PATH = @"C:\img\cheer10k.png";
    private static string CHEER100_IMAGE_PATH = @"C:\img\cheer100k.png";
    private static string PRINTER_NAME = "POS58 Printer";
    private string user, tier, monthStreak, monthTotal, message, totalSubsGifted, subBombCount, reciever, gifts, totalGifts, source;
    private bool fromGiftBomb, anonymous;
    private int bits;
    Image img = Image.FromFile(SUB_IMAGE_PATH);
    public bool Execute()
    {
        source = args["__source"].ToString();
        PrintDialog pd = new PrintDialog();
        PrintDocument pdoc = new PrintDocument();
        PrinterSettings ps = new PrinterSettings();
        Font font = new Font("calibri", 15);
        PaperSize psize = new PaperSize("pprsz", 228, 218);
        pd.Document = pdoc;
        pd.Document.DefaultPageSettings.PaperSize = psize;
        pdoc.DefaultPageSettings.PaperSize.Height = 228;
        pdoc.DefaultPageSettings.PaperSize.Width = 228;
        pdoc.DefaultPageSettings.Margins.Top = 0;
        pdoc.DefaultPageSettings.Margins.Bottom = 0;
        pdoc.DefaultPageSettings.Margins.Left = 0;
        pdoc.DefaultPageSettings.Margins.Right = 0;
        pdoc.PrinterSettings.PrinterName = PRINTER_NAME;
        switch (source)
        {
            case "TwitchSub":
                tier = args["tier"].ToString();
                user = args["user"].ToString();
                message = args["rawInput"].ToString();
                if (tier == "prime")
                    img = Image.FromFile(PRIME_IMAGE_PATH);
                pdoc.PrintPage += new PrintPageEventHandler(twitchSub);
                pdoc.Print();
                pdoc.PrintPage -= new PrintPageEventHandler(twitchSub);
                break;
            case "TwitchReSub":
                tier = args["tier"].ToString();
                user = args["user"].ToString();
                monthStreak = args["monthStreak"].ToString();
                monthTotal = args["cumulative"].ToString();
                message = args["rawInput"].ToString();
                if (tier == "prime")
                    img = Image.FromFile(PRIME_IMAGE_PATH);
                pdoc.PrintPage += new PrintPageEventHandler(twitchReSub);
                pdoc.Print();
                pdoc.PrintPage -= new PrintPageEventHandler(twitchReSub);
                break;
            case "TwitchGiftSub":
                tier = args["tier"].ToString();
                anonymous = Convert.ToBoolean(args["anonymous"]);
                totalSubsGifted = args["totalSubsGifted"].ToString();
                monthStreak = args["monthsGifted"].ToString();
                monthTotal = args["cumulativeMonths"].ToString();
                reciever = args["recipientUser"].ToString();
                fromGiftBomb = Convert.ToBoolean(args["fromGiftBomb"]);
                subBombCount = args["subBombCount"].ToString();
                img = Image.FromFile(GIFT_IMAGE_PATH);
                if (anonymous == false)
                {
                    user = args["user"].ToString();
                }
                else
                {
                    user = "Anonymus";
                }

                pdoc.PrintPage += new PrintPageEventHandler(twitchGiftSub);
                pdoc.Print();
                pdoc.PrintPage -= new PrintPageEventHandler(twitchGiftSub);
                break;
            case "TwitchGiftBomb":
                tier = args["tier"].ToString();
                anonymous = Convert.ToBoolean(args["anonymous"]);
                gifts = args["gifts"].ToString();
                totalGifts = args["totalGifts"].ToString();
                img = Image.FromFile(GIFT_IMAGE_PATH);
                if (anonymous == false)
                {
                    user = args["user"].ToString();
                }
                else
                {
                    user = "Anonymus";
                }

                pdoc.PrintPage += new PrintPageEventHandler(twitchGiftBomb);
                pdoc.Print();
                pdoc.PrintPage -= new PrintPageEventHandler(twitchGiftBomb);
                break;
            case "TwitchCheer":
                bits = Convert.ToInt32(args["bits"]);
                anonymous = Convert.ToBoolean(args["anonymous"]);
                message = args["message"].ToString();
                if (anonymous == false)
                {
                    user = args["user"].ToString();
                }
                else
                {
                    user = "Anonymus";
                }

                if (bits > 99)
                {
                    if (bits < 1000)
                    {
                        img = Image.FromFile(CHEER_IMAGE_PATH);
                    }
                    else
                    {
                        if (bits < 5000)
                        {
                            img = Image.FromFile(CHEER1_IMAGE_PATH);
                        }
                        else
                        {
                            if (bits < 10000)
                            {
                                img = Image.FromFile(CHEER5_IMAGE_PATH);
                            }
                            else
                            {
                                if (bits < 100000)
                                {
                                    img = Image.FromFile(CHEER10_IMAGE_PATH);
                                }
                                else
                                {
                                    img = Image.FromFile(CHEER100_IMAGE_PATH);
                                }
                            }
                        }
                    }

                    pdoc.PrintPage += new PrintPageEventHandler(twitchCheer);
                    pdoc.Print();
                    pdoc.PrintPage -= new PrintPageEventHandler(twitchCheer);
                }

                break;
        }

        return true;
    }

    void twitchSub(object sender, PrintPageEventArgs e)
    {
        Graphics graphics = e.Graphics;
        Font font = new Font("Calibri", 10);
        StringFormat drawFormat = new StringFormat();
        drawFormat.Alignment = StringAlignment.Center;
        drawFormat.Trimming = StringTrimming.Character;
        float fontHeight = font.GetHeight();
        int x = 0;
        int y = 70;
        string line = "";
        string underLine = "-------------------------------------------------------------";
        graphics.DrawImage(img, new Rectangle(0, 0, 190, 90));
        graphics.DrawString(underLine, new Font("calibri", 10), new SolidBrush(Color.Black), x, y += 15);
        graphics.DrawString(user, new Font("M04_FATAL FURY", 10), new SolidBrush(Color.Black), new Rectangle(0, y += 15, 190, 12), drawFormat);
        line = "se ha suscrito.";
        graphics.DrawString(line, new Font("Calibri", 10), new SolidBrush(Color.Black), new Rectangle(0, y += 12, 190, 15), drawFormat);
        graphics.DrawString(message, new Font("Calibri", 6), new SolidBrush(Color.Black), new Rectangle(0, y += 15, 190, 24), drawFormat);
        line = $"{DateTime.Now.ToString("dd/MMMM/yyyy")} Tier: {tier}";
        graphics.DrawString(line, new Font("Calibri", 8), new SolidBrush(Color.Black), x, y += 40);
        graphics.DrawString(underLine, new Font("calibri", 10), new SolidBrush(Color.Black), x, y += 25);
    }

    void twitchReSub(object sender, PrintPageEventArgs e)
    {
        Graphics graphics = e.Graphics;
        Font font = new Font("Calibri", 10);
        StringFormat drawFormat = new StringFormat();
        drawFormat.Alignment = StringAlignment.Center;
        drawFormat.Trimming = StringTrimming.Character;
        float fontHeight = font.GetHeight();
        int x = 0;
        int y = 70;
        string line = "";
        string underLine = "-------------------------------------------------------------";
        graphics.DrawImage(img, new Rectangle(0, 0, 190, 90));
        graphics.DrawString(underLine, new Font("calibri", 10), new SolidBrush(Color.Black), x, y += 15);
        graphics.DrawString(user, new Font("M04_FATAL FURY", 10), new SolidBrush(Color.Black), new Rectangle(0, y += 15, 190, 12), drawFormat);
        line = "se ha vuelto a suscribir.";
        graphics.DrawString(line, new Font("Calibri", 10), new SolidBrush(Color.Black), new Rectangle(0, y += 12, 190, 15), drawFormat);
        graphics.DrawString(message, new Font("Calibri", 6), new SolidBrush(Color.Black), new Rectangle(0, y += 15, 190, 24), drawFormat);
        line = $"{DateTime.Now.ToString("dd/MMMM/yyyy")} Total: {monthTotal} Racha: {monthStreak}";
        graphics.DrawString(line, new Font("Calibri", 8), new SolidBrush(Color.Black), x, y += 40);
        graphics.DrawString(underLine, new Font("calibri", 10), new SolidBrush(Color.Black), x, y += 25);
    }

    void twitchGiftSub(object sender, PrintPageEventArgs e)
    {
        Graphics graphics = e.Graphics;
        Font font = new Font("Calibri", 10);
        StringFormat drawFormat = new StringFormat();
        drawFormat.Alignment = StringAlignment.Center;
        drawFormat.Trimming = StringTrimming.Character;
        float fontHeight = font.GetHeight();
        int x = 0;
        int y = 70;
        string line = "";
        string underLine = "-------------------------------------------------------------";
        graphics.DrawImage(img, new Rectangle(0, 0, 190, 90));
        graphics.DrawString(underLine, new Font("calibri", 10), new SolidBrush(Color.Black), x, y += 15);
        line = $"{user} ha regalado {gifts} SUBS";
        graphics.DrawString(line, new Font("Calibri", 10), new SolidBrush(Color.Black), new Rectangle(0, y += 15, 190, 20), drawFormat);
        graphics.DrawString(reciever, new Font("M04_FATAL FURY", 10), new SolidBrush(Color.Black), new Rectangle(0, y += 20, 190, 12), drawFormat);
        line = $"{DateTime.Now.ToString("dd/MMMM/yyyy")} Total: {totalSubsGifted}";
        graphics.DrawString(line, new Font("Calibri", 8), new SolidBrush(Color.Black), x, y += 40);
        graphics.DrawString(underLine, new Font("calibri", 10), new SolidBrush(Color.Black), x, y += 25);
    }

    void twitchGiftBomb(object sender, PrintPageEventArgs e)
    {
        Graphics graphics = e.Graphics;
        Font font = new Font("Calibri", 10);
        StringFormat drawFormat = new StringFormat();
        drawFormat.Alignment = StringAlignment.Center;
        drawFormat.Trimming = StringTrimming.Character;
        float fontHeight = font.GetHeight();
        int x = 0;
        int y = 70;
        string line = "";
        string underLine = "-------------------------------------------------------------";
        for (int i = 1; i <= Convert.ToInt32(gifts); i++)
        {
            graphics.DrawImage(img, new Rectangle(0, 0, 190, 90));
            graphics.DrawString(underLine, new Font("calibri", 10), new SolidBrush(Color.Black), x, y += 15);
            graphics.DrawString(user, new Font("M04_FATAL FURY", 10), new SolidBrush(Color.Black), new Rectangle(0, y += 20, 190, 12), drawFormat);
            line = $"ha regalado {i} SUB{(i == 1 ? "" : "S")}";
            graphics.DrawString(line, new Font("Calibri", 10), new SolidBrush(Color.Black), new Rectangle(0, y += 15, 190, 20), drawFormat);
            line = $"{DateTime.Now.ToString("dd/MMMM/yyyy")} Total: {Convert.ToInt32(totalGifts) - Convert.ToInt32(gifts) + i}";
            graphics.DrawString(line, new Font("Calibri", 8), new SolidBrush(Color.Black), x, y += 40);
            if (i != Convert.ToInt32(totalSubsGifted))
                graphics.DrawString(underLine, new Font("calibri", 10), new SolidBrush(Color.Black), x, y += 10);
        }

        graphics.DrawString(underLine, new Font("calibri", 10), new SolidBrush(Color.Black), x, y += 25);
    }

    void twitchCheer(object sender, PrintPageEventArgs e)
    {
        Graphics graphics = e.Graphics;
        Font font = new Font("Calibri", 10);
        StringFormat drawFormat = new StringFormat();
        drawFormat.Alignment = StringAlignment.Center;
        drawFormat.Trimming = StringTrimming.Character;
        float fontHeight = font.GetHeight();
        int x = 0;
        int y = 70;
        string line = "";
        string underLine = "-------------------------------------------------------------";
        graphics.DrawImage(img, new Rectangle(0, 0, 190, 90));
        graphics.DrawString(underLine, new Font("calibri", 10), new SolidBrush(Color.Black), x, y += 15);
        graphics.DrawString(user, new Font("M04_FATAL FURY", 10), new SolidBrush(Color.Black), new Rectangle(0, y += 15, 190, 12), drawFormat);
        line = $"ha mandado {bits} bits.";
        graphics.DrawString(line, new Font("Calibri", 10), new SolidBrush(Color.Black), new Rectangle(0, y += 12, 190, 15), drawFormat);
        graphics.DrawString(message, new Font("Calibri", 6), new SolidBrush(Color.Black), new Rectangle(0, y += 15, 190, 24), drawFormat);
        line = $"{DateTime.Now.ToString("dd/MMMM/yyyy")}";
        graphics.DrawString(line, new Font("Calibri", 8), new SolidBrush(Color.Black), x, y += 40);
        graphics.DrawString(underLine, new Font("calibri", 10), new SolidBrush(Color.Black), x, y += 25);
    }
}
