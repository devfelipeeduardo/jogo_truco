namespace TrucoAPI.Models
{
    public class Jogador
    {
        public string Nome { get; set; } = string.Empty;
        public List<Card> Mao { get; set; } = new List<Card>();
        public int Pontos { get; set; } = 0;
    }
}
