public interface MetodoDeDesconto
    {
        double GetDesconto();
    }

public class DescontoPremium : MetodoDeDesconto
    {
        public double GetDesconto() => 0.2;
    }
public class DescontoVip : MetodoDeDesconto
    {
        public double GetDesconto() => 0.1;
    }

public class Pedido {
    private string _nomeCliente { get; set; }
    private double _valor { get; set; }
    private MetodoDeDesconto _metodoDeDesconto;

    public Pedido(MetodoDeDesconto estrategiaDeDesconto, string nomeCliente, double valor) {
        _metodoDeDesconto = estrategiaDeDesconto;
        _nomeCliente = nomeCliente;
        _valor = valor;
    }

    public class CalculadoraDeDesconto
    {
        public double CalcularDesconto(MetodoDeDesconto metodo) {
            if (metodo == null) return 0;
            
            return metodo.GetDesconto() * _valor;
        }
    }

    public void SalvarPedido() {
        Console.WriteLine("Pedido salvo no banco de dados.");
    }

    public void EnviarEmail() {
        Console.WriteLine("Enviando email de confirmação...");
    }
}

