using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CadastroEscolar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
                Sistema de Notas

                Crie uma sistema para uma escola. O sistema deve ajudar a secretaria a 
                cadastrar e alterar as notas de um aluno. 

                O sistema deve ter as seguintes funcionalidades:
                1 - Adicionar um aluno
                2 - Acessar o perfil de um aluno
                3 - No perfil do aluno, a opção de inserir notas e verificar a media 
                final do aluno se possível. Deve ser possível inserir até 4 notas para o aluno.

                5 - O sistema deve possuir um menu que mostre as opções:
                    1 - Adicionar Aluno
                    2 - Lista de Alunos
                    3 - Sair 
            */
            const int QUANTIDADE_ALUNOS = 20;

            int operacao = 0;
            string[] nomes = new string[QUANTIDADE_ALUNOS];
            string[] sexos = new string[QUANTIDADE_ALUNOS];
            int[] idades = new int[QUANTIDADE_ALUNOS];
            decimal?[,] notas = new decimal?[QUANTIDADE_ALUNOS, 4];

            int ultimoAluno = 0;
            int alunoSelecionado = 0;

            do
            {
                operacao = 0;

                // Menu opções
                if (operacao == 0)
                {
                    Console.Clear();
                    Console.WriteLine("===========================");
                    Console.WriteLine("=                         =");
                    Console.WriteLine("=         CADASTRO        =");
                    Console.WriteLine("=          ESCOLAR        =");
                    Console.WriteLine("=                         =");
                    Console.WriteLine("===========================");

                    Console.WriteLine("Menu de opções:");
                    Console.WriteLine("1 - Adicionar aluno");
                    Console.WriteLine("2 - Listar Alunos");
                    Console.WriteLine("9 - Sair");

                    Console.Write("Digite a opção:");
                    operacao = (int)SolicitaInformacao("", "int");
                    Console.Clear();
                }

                // Cadastrar Aluno
                if (operacao == 1)
                {
                    if (ultimoAluno == nomes.Length)
                    {
                        Console.WriteLine("Foi atingido o limite de alunos a serem cadastrados");
                        Thread.Sleep(2000);

                        // Pula para proxima iteração do while;
                        continue;
                    }

                    Console.WriteLine("Digite as informações do aluno:");

                    string nome = (string)SolicitaInformacao("Qual o nome do aluno?", "string");
                    string sexo = (string)SolicitaInformacao("Qual o sexo do aluno? (M/F)", "string");
                    int idade = (int)SolicitaInformacao("Qual a idade do aluno?", "int");

                    nomes[ultimoAluno] = nome;
                    sexos[ultimoAluno] = sexo;
                    idades[ultimoAluno] = idade;

                    ultimoAluno++;
                }

                // Lista de alunos
                if (operacao == 2)
                {
                    Console.WriteLine("======LISTA DE ALUNOS======");

                    for(int index = 0; index < nomes.Length; index++)
                    {
                        // Se não cadastrou o aluno ele pula para o proximo
                        if (nomes[index] == null)
                            continue;

                        Console.WriteLine($"{index + 1} - {nomes[index]}");
                    }

                    Console.Write("Digite o numero do aluno, ou 0 para voltar ao menu:");
                    alunoSelecionado = (int)SolicitaInformacao("", "int");

                    if (alunoSelecionado > 0 && alunoSelecionado <= QUANTIDADE_ALUNOS)
                    {
                        // Código para o perfil do aluno
                        operacao = 4;
                    }
                }

                // Perfil do Aluno
                if (operacao == 4)
                {
                    string operacaoNotas = "not";

                    if (alunoSelecionado > 0)
                        alunoSelecionado--;
                    else
                        continue;

                    do
                    {
                        Console.Clear();

                        Console.WriteLine("=======PERFIL DO ALUNO=======");
                        Console.WriteLine($"Nome: {nomes[alunoSelecionado]}");
                        Console.WriteLine($"Idade: {idades[alunoSelecionado]}");
                        Console.WriteLine($"Sexo: {sexos[alunoSelecionado]}");

                        Console.WriteLine("=======NOTAS DO ALUNO=======");

                        if (operacaoNotas.ToUpper() != "S")
                        {
                            for (int index = 0; index < 4; index++)
                            {
                                Console.Write($"Nota {index + 1} - ");

                                if (notas[alunoSelecionado, index] == null)
                                    Console.WriteLine("N/A");
                                else
                                    Console.WriteLine(notas[alunoSelecionado, index]);
                            }

                            // Calcular a média
                            decimal? media = null;

                            // Passa por cada uma das notas para somar na média
                            for (int index = 0; index < 4; index++)
                            {
                                // Verifica se a nota é nula, então para o for.
                                if (notas[alunoSelecionado, index] == null)
                                {
                                    media = null;
                                    break;
                                } 
                                else
                                {
                                    media = media == null ? 0 : media;
                                }

                                // Soma a nota na média
                                media += notas[alunoSelecionado, index];
                            }

                            // Se nenhuma nota é nula, divide por 4 a media e mostra na tela.
                            if (media != null)
                            {
                                media /= 4;
                                Console.WriteLine("A média é " + media.ToString());
                            }
                        }
                        else
                        {
                            for (int index = 0; index < 4; index++)
                            {
                                decimal nota = (decimal)SolicitaInformacao($"Qual a nota {index + 1}", "decimal");
                                notas[alunoSelecionado, index] = nota;
                            }
                        }

                        operacaoNotas = (string)SolicitaInformacao("Deseja cadastrar notas (S/N)?", "string");
                    } while (operacaoNotas.ToUpper() == "S");
                }

            } while (operacao != 9);
        }

        //
        // Resumo:
        //     Função que mostra uma mensagem e retorna um valor informado pelo console
        //
        // Parâmetros:
        //   mensagem:
        //     A mensagem a ser mostrada
        //
        //   tipo:
        //     O tipo de retorno (decimal, int, string)
        //
        static object SolicitaInformacao(string mensagem, string tipo)
        {
            decimal valor_decimal;
            int valor_inteiro;

            // Verifica se a mensagem tem mais de 0 caracteres
            if (mensagem.Length > 0)
                Console.WriteLine(mensagem);

            if (tipo == "string")
                return Console.ReadLine();
            else if (tipo == "decimal")
            {
                decimal.TryParse(Console.ReadLine(), out valor_decimal);
                return valor_decimal;
            }
            else if (tipo == "int")
            {
                int.TryParse(Console.ReadLine(), out valor_inteiro);
                return valor_inteiro;
            }

            return "";
        }
    }
}
