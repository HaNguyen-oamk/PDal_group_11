using UnityEngine;

public class VoiceCommandController : MonoBehaviour
{
    private SharkController shark;
    private PlayerShooting shooter;

    // tránh lặp lệnh liên tục
    private string lastCommand = "";
    private float commandCooldown = 0.3f;
    private float nextCommandTime = 0f;

    void Start()
    {
        shark = FindObjectOfType<SharkController>();
        shooter = FindObjectOfType<PlayerShooting>();

        if (shark == null)
            Debug.LogWarning("⚠ SharkController NOT FOUND in scene!");

        if (shooter == null)
            Debug.LogWarning("⚠ PlayerShooting NOT FOUND in scene!");
    }

    // Hàm fuzzy sửa lỗi giọng Việt nói tiếng Anh
    string Normalize(string cmd)
    {
        cmd = cmd.ToLower().Trim();

        // Fire (shoot)
        if (
            cmd.Contains("shoot") ||
            cmd.Contains("shit") || 
            cmd.Contains("shut") || 
            cmd.Contains("sheet") ||
            cmd.Contains("shot"))
            return "shoot";

        // Left
        if (cmd.Contains("left") ||
            cmd.Contains("lef") ||
            cmd.Contains("lep") ||
            cmd.Contains("letf") ||
            cmd.Contains("let"))
            return "left";

        // Right
        if (cmd.Contains("right") ||
            cmd.Contains("rit") ||
            cmd.Contains("rait"))
            return "right";

        // Run
        if (cmd.Contains("run") ||
            cmd.Contains("ran") ||
            cmd.Contains("ron"))
            return "run";

        // Walk
        if (cmd.Contains("walk") ||
            cmd.Contains("wok") ||
            cmd.Contains("wolk"))
            return "walk";

        return cmd;
    }

    public void ExecuteCommand(string command)
    {
        if (Time.time < nextCommandTime) return;

        command = Normalize(command);

        if (command == lastCommand) return;
        lastCommand = command;
        nextCommandTime = Time.time + commandCooldown;

        Debug.Log("⚡ VOICE COMMAND: " + command);

        if (shark == null || shooter == null) return;

        switch (command)
        {
            case "run":
                shark.speed = 5f;
                break;

            case "walk":
                shark.speed = 3f;
                break;

            case "shoot":
                shooter.Shoot();
                break;

            case "left":
                shark.transform.localScale = new Vector3(-1, 1, 1);
                break;

            case "right":
                shark.transform.localScale = new Vector3(1, 1, 1);
                break;
        }
    }
}
