using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Game.Scripts
{
    public enum GameState
    {
        SEQUENCIA,
        RESPONDER,
        NOVA,
        ERRO
    }
    [RequireComponent(typeof(AudioSource))]
    class GameController: MonoBehaviour
    {
        [SerializeField] GameState gameState;

        public GameState GameState
        {
            set => gameState = value;
            get => gameState;
        }
        [Header("Textos informativo")]
        [SerializeField] Text rodadaTxt;
        [SerializeField] Text qtdNotasTxt;

        [Header("Componentes do game")]
        [SerializeField] Color[] cor;
        [SerializeField] Image[] botoes;
        [SerializeField] GameObject startButton;

        [SerializeField] List<int> cores; // sequencia de cores dos botões

        [Header("Variáveis de controle")]
        [SerializeField] int idResposta;
        [SerializeField] int qtdCores;
        [SerializeField] int rodada;

        [Header("Sons")]
        [SerializeField] AudioClip[] sons;
        private AudioSource fonteAudio;

        private void Start()
        {
            fonteAudio = GetComponent<AudioSource>();
            NovaRodada();
        }

        public void StartGame()
        {
            StartCoroutine(Sequencia(qtdCores + rodada));
        }

        public void NovaRodada()
        {
            foreach (Image image in botoes)
                image.color = cor[0];

            cores.Clear();
            startButton.SetActive(true);
            rodadaTxt.text = $"Rodada: {rodada + 1}";
            qtdNotasTxt.text = $"Sequência: {qtdCores + rodada}";
        }
        IEnumerator Sequencia(int qtd)
        {
            startButton.SetActive(false);
            for(int i = qtd; i > 0; i--)
            {
                yield return new WaitForSeconds(0.5f);
                int randomico = Random.Range(0, botoes.Length);
                botoes[randomico].color = cor[1];
                fonteAudio.PlayOneShot(sons[randomico]);
                cores.Add(randomico);
                yield return new WaitForSeconds(0.5f);
                botoes[randomico].color = cor[0];
            }
            GameState = GameState.RESPONDER;
            idResposta = 0;
        }

        public IEnumerator Responder(int idButao)
        {
            botoes[idButao].color = cor[1];
            if (cores[idResposta] == idButao)
                fonteAudio.PlayOneShot(sons[idButao]);
            else
            {
                GameState = GameState.ERRO;
                StartCoroutine(GameOver());
            }
            
            idResposta++;

            if (idResposta == cores.Count)
            {
                GameState = GameState.NOVA;
                rodada++;
                yield return new WaitForSeconds(0.1f);
                NovaRodada();
            }

            yield return new WaitForSeconds(0.3f);
            botoes[idButao].color = cor[0];
        }
        IEnumerator GameOver()
        {
            rodada = 0;
            fonteAudio.PlayOneShot(sons[4]);
            yield return new WaitForSeconds(1);
            for (int i = 3; i > 0; i--)
            {
                foreach (Image img in botoes)
                    img.color = cor[1];
                
                yield return new WaitForSeconds(0.3f);

                foreach (Image img in botoes)
                    img.color = cor[0];
                
                yield return new WaitForSeconds(0.3f);

            }
            for(int i = 0; i < 3; i++)
                foreach (Image img in botoes)
                {
                    img.color = cor[1];
                    yield return new WaitForSeconds(0.1f);
                    img.color = cor[0];
                }
            GameState = GameState.NOVA;
            NovaRodada();
        }
    }
}
