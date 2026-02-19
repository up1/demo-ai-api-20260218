public class TennisGame1
{
    private int m_score1;
    private int m_score2;
    private readonly string player1Name;
    private readonly string player2Name;

    public TennisGame1(string player1Name, string player2Name)
    {
        this.player1Name = player1Name;
        this.player2Name = player2Name;
    }

    public void WonPoint(string playerName)
    {
        if (playerName == player1Name)
            m_score1++;
        else
            m_score2++;
    }

    public string GetScore()
    {
        if (m_score1 == m_score2)
        {
            return m_score1 switch
            {
                0 => "Love-All",
                1 => "Fifteen-All",
                2 => "Thirty-All",
                _ => "Deuce"
            };
        }

        if (m_score1 >= 4 || m_score2 >= 4)
        {
            int diff = m_score1 - m_score2;
            return diff switch
            {
                1 => $"Advantage {player1Name}",
                -1 => $"Advantage {player2Name}",
                >= 2 => $"Win for {player1Name}",
                _ => $"Win for {player2Name}"
            };
        }

        string[] scoreNames = ["Love", "Fifteen", "Thirty", "Forty"];
        return $"{scoreNames[m_score1]}-{scoreNames[m_score2]}";
    }
}