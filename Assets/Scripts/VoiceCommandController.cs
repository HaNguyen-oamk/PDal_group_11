using UnityEngine;

public class VoiceCommandController : MonoBehaviour
{
    private SharkController shark;
    private PlayerShooting shooter;

    string lastCommand = "";
    float commandCooldown = 0.2f;
    float nextCommandTime = 0f;

    void Start()
    {
        shark = FindObjectOfType<SharkController>();
        shooter = FindObjectOfType<PlayerShooting>();

        if (shark == null)
            Debug.LogWarning("⚠ SharkController NOT FOUND!");
        if (shooter == null)
            Debug.LogWarning("⚠ PlayerShooting NOT FOUND!");
    }

    string Normalize(string cmd)
    {
        cmd = cmd.ToLower().Trim();

        // TURN first **
        if (cmd.Contains("turn left"))
            return "turn_left";

        if (cmd.Contains("turn right"))
            return "turn_right";

        // MOVE
        if (cmd.Contains("go up") || cmd.Contains("move up") || cmd == "up")
            return "go_up";

        if (cmd.Contains("go down") || cmd.Contains("move down") || cmd.Contains("no down") || cmd.Contains("down") || cmd.Contains("no doubt")  || cmd.Contains("now"))
            return "go_down";

        if (cmd.Contains("go left") || cmd.Contains("move left") || cmd == "left")
            return "go_left";

        if (cmd.Contains("go right") || cmd.Contains("move right") || cmd == "right")
            return "go_right";

        // STOP
        if (cmd.Contains("stop"))
            return "stop";

        // SPEED
        if (cmd.Contains("run") || cmd.Contains("rob"))
            return "run";

        if (cmd.Contains("walk") || cmd.Contains("slow"))
            return "walk";

        // SHOOT / ATTACK
        if (cmd.Contains("shoot") || cmd.Contains("fire") ||
            cmd.Contains("attack") || cmd.Contains("hit") || cmd.Contains("bite"))
            return "attack";

        return cmd;
    }

    public void ExecuteCommand(string command)
    {
        if (Time.time < nextCommandTime) return;

        command = Normalize(command);

        if (command == lastCommand) return;
        lastCommand = command;
        nextCommandTime = Time.time + commandCooldown;

        Debug.Log("VOICE CMD: " + command);

        if (shark == null) return;

        switch (command)
        {
            case "run":
                shark.currentSpeed = shark.runSpeed;
                break;

            case "walk":
                shark.currentSpeed = shark.walkSpeed;
                break;

            case "go_up":
                shark.voiceInput = Vector2.up;
                shark.voiceActive = true;
                break;

            case "go_down":
                shark.voiceInput = Vector2.down;
                shark.voiceActive = true;
                break;

            case "go_left":
                shark.voiceInput = Vector2.left;
                shark.voiceActive = true;
                shark.GetComponent<SpriteRenderer>().flipX = true;
                break;

            case "go_right":
                shark.voiceInput = Vector2.right;
                shark.voiceActive = true;
                shark.GetComponent<SpriteRenderer>().flipX = false;
                break;

            case "turn_left":
                shark.voiceActive = false;
                shark.voiceInput = Vector2.zero;
                shark.GetComponent<SpriteRenderer>().flipX = true;
                break;

            case "turn_right":
                shark.voiceActive = false;
                shark.voiceInput = Vector2.zero;
                shark.GetComponent<SpriteRenderer>().flipX = false;
                break;

            case "stop":
                shark.StopMovement();
                break;

            case "attack":   // SHOOT like SPACE
                shark.TriggerAttack();       // animation + logic 
                if (shooter != null)
                    shooter.Shoot();        // shoot bullet
                break;
        }
    }
}
