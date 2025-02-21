using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatMenu : MonoBehaviour
{
    // Metodo per il pulsante RESTART: ricarica la scena di gioco
    public void RestartGame()
    {
        // Opzione 1: ricaricare la scena attiva (se il gameplay e la schermata di sconfitta sono nella stessa scena)
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Opzione 2: caricare una specifica scena di gioco
        SceneManager.LoadScene("Level design"); // Sostituisci "NomeScenaDiGioco" con il nome esatto della tua scena di gioco
    }

    // Metodo per il pulsante EXIT: chiude l'applicazione
    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
