# TennisGame1 - `GetScore()` Explanation

## Code Explanation

The `GetScore()` method returns the current tennis score as a string, following standard tennis scoring terminology. It handles three distinct scenarios:

### 1. Equal Scores (`m_score1 == m_score2`)
When both players have the same score:
| Points | Result |
|--------|-------------|
| 0 | "Love-All" |
| 1 | "Fifteen-All" |
| 2 | "Thirty-All" |
| 3+ | "Deuce" |

### 2. Endgame (`m_score1 >= 4` or `m_score2 >= 4`)
When at least one player has 4+ points, the difference determines the result:
| Difference (p1 - p2) | Result |
|-----------------------|------------------------|
| +1 | "Advantage player1" |
| -1 | "Advantage player2" |
| >= +2 | "Win for player1" |
| <= -2 | "Win for player2" |

### 3. Normal Scoring (scores differ, both < 4)
Each player's numeric score is converted to a tennis term (`0→Love`, `1→Fifteen`, `2→Thirty`, `3→Forty`) and joined with a hyphen. Example: `"Fifteen-Love"`, `"Thirty-Forty"`.

---

## Flowchart

```mermaid
flowchart TD
    Start([GetScore called]) --> EqualCheck{m_score1 == m_score2?}

    EqualCheck -->|Yes| EqualSwitch{m_score1 value?}
    EqualSwitch -->|0| LoveAll["score = 'Love-All'"]
    EqualSwitch -->|1| FifteenAll["score = 'Fifteen-All'"]
    EqualSwitch -->|2| ThirtyAll["score = 'Thirty-All'"]
    EqualSwitch -->|3+| Deuce["score = 'Deuce'"]

    EqualCheck -->|No| EndgameCheck{m_score1 >= 4 OR m_score2 >= 4?}

    EndgameCheck -->|Yes| CalcDiff["diff = m_score1 - m_score2"]
    CalcDiff --> DiffCheck{diff value?}
    DiffCheck -->|+1| AdvP1["score = 'Advantage player1'"]
    DiffCheck -->|-1| AdvP2["score = 'Advantage player2'"]
    DiffCheck -->|>= +2| WinP1["score = 'Win for player1'"]
    DiffCheck -->|<= -2| WinP2["score = 'Win for player2'"]

    EndgameCheck -->|No| NormalScore["Convert each player's points\n0→Love, 1→Fifteen,\n2→Thirty, 3→Forty"]
    NormalScore --> JoinScores["score = player1Term + '-' + player2Term\ne.g. 'Fifteen-Love'"]

    LoveAll --> Return([return score])
    FifteenAll --> Return
    ThirtyAll --> Return
    Deuce --> Return
    AdvP1 --> Return
    AdvP2 --> Return
    WinP1 --> Return
    WinP2 --> Return
    JoinScores --> Return
```
