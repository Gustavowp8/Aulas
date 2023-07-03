using System;
using OlaBanco.data;
using System.Threading;

namespace OlaBanco{
    class Program{
        static void Main(string[] args){

            Console.WriteLine("Tarefas cadastradas:");

            int numTarefas = 0;

            using (var db = new TarefasContext())
            {
                var tarefas = db.Tarefa;

                numTarefas = tarefas.Count();
            }

            Console.WriteLine($"Foram encontradas {numTarefas} tarefas.");
            Console.WriteLine();


            Thread.Sleep(1500);
            Console.Clear();
            Menu();
        }

        static void Menu(){
            Console.WriteLine();
             Console.WriteLine("****** MENU ******");

            string g;
            Console.WriteLine();

            Console.WriteLine("1 - Lista de tarefas");
            Console.WriteLine("2 - Tarefas Pendentes");
            Console.WriteLine("3 - Buscar por descrição");
            Console.WriteLine("4 - Buscar por ID");
            Console.WriteLine("5 - Incluir nova tarefa");
            Console.WriteLine("6 - Alterar tarefa");
            Console.WriteLine("7 - Concluir tarefa");
            Console.WriteLine("8 - Excluir Tarefa");
            Console.WriteLine("0 - Sair do programa");
            Console.WriteLine();

            Console.Write("Opção: ");
            g = Console.ReadLine();

            switch(g){
                case "1": ListaTarefas(); break;
                case "2": TarefasPendentes(); break;
                case "3": ListarPorDescricao(); break;
                case "4": ListaTarefasPorId(); break;
                case "5": Incuir(); break;
                case "6": AlterarTarefa(); break;
                case "7": Concluir(); break;
                case "8": Excluir(); break;
                case "0": Sair(); break;
                default: Menu(); break;
            }
        }

        static void ListaTarefas(){
            Console.Clear();
            Console.WriteLine("Listando Tarefas");

            using (var db = new TarefasContext())
            {
                var tarefas = db.Tarefa.ToList<Tarefa>();

                Console.WriteLine($"{tarefas.Count} tarefa(s) encontradas(s).");

                foreach(var tarefa in tarefas)
                {
                    Console.WriteLine($"[{(tarefa.Concluida ? "X" : " ")}] #{tarefa.Id}: {tarefa.Descricao}");
                }

            }
            System.Console.WriteLine();
            Console.WriteLine("Precione qualquer tecla para continuar...");
            Console.ReadKey();
            Menu();
        }

        static void TarefasPendentes(){
            Console.Clear();
            Console.WriteLine("Tarefas pendentes");
            Console.WriteLine();

            using (var db = new TarefasContext())
            {
                var tarefas = db.Tarefa
                .Where(t => !t.Concluida)
                .OrderByDescending(t => t.Id)
                .ToList<Tarefa>();

                Console.WriteLine($"{tarefas.Count} tarefa(s) encontradas(s).");

                foreach(var tarefa in tarefas)
                {
                    Console.WriteLine($"[{(tarefa.Concluida ? "X" : " ")}] #{tarefa.Id}: {tarefa.Descricao}");
                }
            }

            //opções para volta ao menu
            System.Console.WriteLine();
            Console.WriteLine("Precione qualquer tecla para continuar...");
            Console.ReadKey();
            Menu();
        }

        static void ListarPorDescricao()
        {
            Console.Clear();
            
            Console.WriteLine("Busacar por descrição");
            Console.WriteLine();
            Console.Write("Buscar por: ");
            string descricao = Console.ReadLine();

            using (var db = new TarefasContext())
            {
                var tarefas = db.Tarefa
                .Where(t => t.Descricao.Contains(descricao))
                .OrderBy(t => t.Descricao)
                .ToList<Tarefa>();

                Console.WriteLine($"{tarefas.Count} tarefa(s) encontradas(s).");

                foreach(var tarefa in tarefas)
                {
                    Console.WriteLine($"[{(tarefa.Concluida ? "X" : " ")}] #{tarefa.Id}: {tarefa.Descricao}");
                }
            }

            //opções para volta ao menu
            System.Console.WriteLine();
            Console.WriteLine("Precione qualquer tecla para continuar...");
            Console.ReadKey();
            Menu();
        }

        static void ListaTarefasPorId()
        {
            Console.Clear();

            Console.WriteLine("Lista tarefas por ID");
            Console.WriteLine();
            Console.Write("Digite o ID: ");
            int id = int.Parse(Console.ReadLine());

             using (var db = new TarefasContext())
            {
                var tarefa = db.Tarefa.Find(id);

                if(tarefa == null)
                {
                    System.Console.WriteLine("Tarefa não encontrada");
                    Menu();
                    return;
                }

                Console.WriteLine($"[{(tarefa.Concluida ? "X" : " ")}] #{tarefa.Id}: {tarefa.Descricao}");
            }

            //opções para volta ao menu
            System.Console.WriteLine();
            Console.WriteLine("Precione qualquer tecla para continuar...");
            Console.ReadKey();
            Menu();

        }

        static void Sair(){
            Console.Clear();
            Console.WriteLine("Obrigado por usar este programa");
        }

         //Realizndo o Crud

        static void Incuir()
        {
            Console.Clear();
            Console.WriteLine("Incluir nova tarefa");
            Console.WriteLine();

            Console.Write("Descrição da tarefa: ");
            string descricao = Console.ReadLine();

            if(String.IsNullOrEmpty(descricao))
            {
                Console.WriteLine("Não e possivel incluir tarefa sem descrição");

                System.Console.WriteLine();
                Console.WriteLine("Precione qualquer tecla para continuar...");
                Console.ReadKey();
                Menu();
                return;
            }

            using(var db = new TarefasContext())
            {
                /*var tarefa = new Tarefa();
                tarefa.Descricao = descricao*/

                var tarefa = new Tarefa{
                    Descricao = descricao
                };
                db.Tarefa.Add(tarefa);
                db.SaveChanges();

                System.Console.WriteLine();
                Console.WriteLine($"[{(tarefa.Concluida ? "X" : " ")}] #{tarefa.Id}: {tarefa.Descricao}, FOI SALVO COM SUCESSO!!!");
                System.Console.WriteLine();
            }

            System.Console.WriteLine();
            Console.WriteLine("Precione qualquer tecla para continuar...");
            Console.ReadKey();
            Menu();

        }

        static void AlterarTarefa()
        {
            Console.Clear();
            Console.WriteLine("Alterar Descrição da Tarefa");

            Console.WriteLine();
            Console.Write("Digite o ID da Tarefa: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.Write("Digite a nova descrição: ");
            string descricao = Console.ReadLine();

            if(String.IsNullOrEmpty(descricao))
            {
                Console.WriteLine("Não e permitido deixar a tarefa sem descrição");
                return;
            }

            //Busca por ID
            using(var db = new TarefasContext())
            {
                var tarefa = db.Tarefa.Find(id);

                if(tarefa == null)
                {
                    Console.WriteLine("Tarefa não encontrada");
                    return;
                }
                //Alteração
                tarefa.Descricao = descricao;
                db.SaveChanges();

                Console.WriteLine($"[{(tarefa.Concluida ? "X" : " ")}] #{tarefa.Id}: {tarefa.Descricao}");
            }

            System.Console.WriteLine();
            Console.WriteLine("Precione qualquer tecla para continuar...");
            Console.ReadKey();
            Menu();
        }

        static void Concluir()
        {
            Console.Clear();
            Console.WriteLine("Concluir tarefa");

            Console.WriteLine();

            Console.Write("Digite o ID da tarefa que você deseja concluir: ");
            int id = int.Parse(Console.ReadLine());

            using(var db = new TarefasContext())
            {
                var tarefa = db.Tarefa.Find(id);

                if(tarefa == null)
                {
                    Console.WriteLine("Tarefa não encontrada");
                    Console.ReadKey();
                    Menu();
                    return;
                }

                if(tarefa.Concluida)
                {
                    Console.WriteLine("Tarefa já concluida");
                    Console.ReadKey();
                    Menu();
                    return;
                }

                tarefa.Concluida = true;
                db.SaveChanges();
                Console.WriteLine($"[{(tarefa.Concluida ? "X" : " ")}] #{tarefa.Id}: {tarefa.Descricao}, tarefa concluida com sucesso!!!");
            }

            //opções para volta ao menu
            System.Console.WriteLine();
            Console.WriteLine("Precione qualquer tecla para continuar...");
            Console.ReadKey();
            Menu();
        }

        static void Excluir()
        {
            Console.Clear();
            Console.WriteLine("Excluir tarefa");
            System.Console.WriteLine();

            Console.Write("Digite o ID da tarefa que você deseja excluir: ");
            int id = int.Parse(Console.ReadLine());

            using(var db = new TarefasContext())
            {
                var tarefa = db.Tarefa.Find(id);

                if(tarefa == null)
                {
                    Console.WriteLine("Tarefa não encontrada");
                    Console.ReadKey();
                    Menu();
                    return;
                }
                //Excluir
                db.Tarefa.Remove(tarefa);
                db.SaveChanges();

                Console.WriteLine("Tarefa excluida com sucesso!!!");
            
            }

            //opções para volta ao menu
            System.Console.WriteLine();
            Console.WriteLine("Precione qualquer tecla para continuar...");
            Console.ReadKey();
            Menu();
        }
    }
}