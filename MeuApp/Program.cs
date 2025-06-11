using System;
using System.Collections.Generic;
using System.Linq;

class Medico
{
    public string Nome { get; set; }
    public string Especialidade { get; set; }
    public string CRM { get; set; }
}

class Paciente
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }
}

class Consulta
{
    public Medico Medico { get; set; }
    public Paciente Paciente { get; set; }
    public DateTime Data { get; set; }
}

class Program
{
    static List<Medico> medicos = new();
    static List<Paciente> pacientes = new();
    static List<Consulta> consultas = new();

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Sistema de Clínica Médica ===");
            Console.WriteLine("1. Cadastrar Médico");
            Console.WriteLine("2. Cadastrar Paciente");
            Console.WriteLine("3. Agendar Consulta");
            Console.WriteLine("4. Listar Consultas");
            Console.WriteLine("5. Buscar Consultas");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1": CadastrarMedico(); break;
                case "2": CadastrarPaciente(); break;
                case "3": AgendarConsulta(); break;
                case "4": ListarConsultas(); break;
                case "5": BuscarConsultas(); break;
                case "0": return;
                default: Console.WriteLine("Opção inválida!"); break;
            }

            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ReadLine();
        }
    }

    static void CadastrarMedico()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine();
        Console.Write("Especialidade: ");
        string esp = Console.ReadLine();
        Console.Write("CRM: ");
        string crm = Console.ReadLine();

        medicos.Add(new Medico { Nome = nome, Especialidade = esp, CRM = crm });
        Console.WriteLine("Médico cadastrado com sucesso!");
    }

    static void CadastrarPaciente()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine();
        Console.Write("Data de nascimento (dd/mm/aaaa): ");
        DateTime nascimento = DateTime.Parse(Console.ReadLine());
        Console.Write("CPF: ");
        string cpf = Console.ReadLine();

        pacientes.Add(new Paciente { Nome = nome, DataNascimento = nascimento, CPF = cpf });
        Console.WriteLine("Paciente cadastrado com sucesso!");
    }

    static void AgendarConsulta()
    {
        if (medicos.Count == 0 || pacientes.Count == 0)
        {
            Console.WriteLine("É necessário ter ao menos um médico e um paciente cadastrados.");
            return;
        }

        Console.WriteLine("Escolha o número do médico:");
        for (int i = 0; i < medicos.Count; i++)
            Console.WriteLine($"{i} - {medicos[i].Nome} ({medicos[i].Especialidade})");

        int indexMedico = int.Parse(Console.ReadLine());

        Console.WriteLine("Escolha o número do paciente:");
        for (int i = 0; i < pacientes.Count; i++)
            Console.WriteLine($"{i} - {pacientes[i].Nome}");

        int indexPaciente = int.Parse(Console.ReadLine());

        Console.Write("Data da consulta (dd/mm/aaaa hh:mm): ");
        DateTime data = DateTime.Parse(Console.ReadLine());

        consultas.Add(new Consulta
        {
            Medico = medicos[indexMedico],
            Paciente = pacientes[indexPaciente],
            Data = data
        });

        Console.WriteLine("Consulta agendada com sucesso!");
    }

    static void ListarConsultas()
    {
        if (consultas.Count == 0)
        {
            Console.WriteLine("Nenhuma consulta agendada.");
            return;
        }

        foreach (var c in consultas)
        {
            Console.WriteLine($"Consulta em {c.Data:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Médico: {c.Medico.Nome} ({c.Medico.Especialidade})");
            Console.WriteLine($"Paciente: {c.Paciente.Nome}");
            Console.WriteLine("-------------------------------------");
        }
    }

    static void BuscarConsultas()
    {
        Console.Write("Buscar por (1-Médico | 2-Paciente | 3-Data): ");
        string op = Console.ReadLine();

        IEnumerable<Consulta> resultados = new List<Consulta>();

        switch (op)
        {
            case "1":
                Console.Write("Nome do médico: ");
                string nomeMed = Console.ReadLine().ToLower();
                resultados = consultas.Where(c => c.Medico.Nome.ToLower().Contains(nomeMed));
                break;

            case "2":
                Console.Write("Nome do paciente: ");
                string nomePac = Console.ReadLine().ToLower();
                resultados = consultas.Where(c => c.Paciente.Nome.ToLower().Contains(nomePac));
                break;

            case "3":
                Console.Write("Data da consulta (dd/mm/aaaa): ");
                DateTime data = DateTime.Parse(Console.ReadLine());
                resultados = consultas.Where(c => c.Data.Date == data.Date);
                break;

            default:
                Console.WriteLine("Opção inválida.");
                return;
        }

        foreach (var c in resultados)
        {
            Console.WriteLine($"Consulta: {c.Data:dd/MM/yyyy HH:mm} - Dr(a). '{c.Medico.Nome}' com paciente '{c.Paciente.Nome}'");
        }

        if (!resultados.Any())
            Console.WriteLine("Nenhuma consulta encontrada.");
    }
}
