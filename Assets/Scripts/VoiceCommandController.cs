using UnityEngine;
using System.Collections; // use for move set time(Invoke)

public class VoiceCommandController : MonoBehaviour
{
    private SharkController shark;
    private PlayerShooting shooter;

    string lastCommand = "";
    float commandCooldown = 0.2f;
    float nextCommandTime = 0f;

    // How long voice-based movement should last before auto-stopping
    public float voiceMoveDuration = 5f;


    void Start()
    {
        shark = FindObjectOfType<SharkController>();
        shooter = FindObjectOfType<PlayerShooting>();

        if (shark == null)
            Debug.LogWarning("SharkController NOT FOUND!");

        if (shooter == null)
            Debug.LogWarning("PlayerShooting NOT FOUND!");
    }

    // Normalizes the recognized speech into a unified command keyword
    string Normalize(string cmd)
    {
        cmd = cmd.ToLower().Trim();

        // Turning
        if (cmd.Contains("turn left"))
            return "turn_left";

        if (cmd.Contains("turn right"))
            return "turn_right";

        // Movement
        if (cmd.Contains("go up") || cmd.Contains("move up") || cmd.Contains("no up") || cmd.Contains("up"))
            return "go_up";

        if (cmd.Contains("go down") || cmd.Contains("move down") || cmd.Contains("no down") || cmd.Contains("down") || cmd.Contains("now")|| cmd.Contains("no doubt"))
            return "go_down";

        if (cmd.Contains("go left") || cmd.Contains("move left") || cmd.Contains("no left") || cmd.Contains("left") || cmd.Contains("let"))
            return "go_left";

        if (cmd.Contains("go right") || cmd.Contains("move right") || cmd.Contains("no right") || cmd.Contains("right"))
            return "go_right";

        // Stop movement
        if (cmd.Contains("stop"))
            return "stop";

        // Speed changes
        if (cmd.Contains("run"))
            return "run";

        if (cmd.Contains("walk") || cmd.Contains("wall"))
            return "walk";

        // Attack / Shooting
        if (cmd.Contains("shoot") || cmd.Contains("fire") ||
            cmd.Contains("five") || cmd.Contains("shit") ||
            cmd.Contains("fine"))
            return "attack";

        return cmd;
    }

    // Executes a directional movement command and sets an auto-stop timer
    void ExecuteMoveCommand(Vector2 direction)
    {
        shark.voiceInput = direction;
        shark.voiceActive = true;

        // Apply the voice movement speed
        shark.activeVoiceSpeed = shark.voiceMoveSpeed;

        // Reset the auto-stop timer
        CancelInvoke(nameof(StopMovementAfterDelay));
        Invoke(nameof(StopMovementAfterDelay), voiceMoveDuration);
    }

    // Automatically stop movement after voiceMoveDuration
    void StopMovementAfterDelay()
    {
        if (shark != null && shark.voiceActive)
        {
            shark.StopMovement();
            Debug.Log("VOICE CMD: Auto Stop (Duration Reached)");
        }
    }

    // Main entry point — executes a recognized voice command
    public void ExecuteCommand(string command)
    {
        // Anti-spam / cooldown
        if (Time.time < nextCommandTime) return;

        command = Normalize(command);

        // Prevent repeated commands
        if (command == lastCommand) return;
        lastCommand = command;
        nextCommandTime = Time.time + commandCooldown;

        Debug.Log("VOICE CMD: " + command);

        if (shark == null) return;

        // Cancel auto-stop if new command is not movement
        if (command != "go_up" && command != "go_down" &&
            command != "go_left" && command != "go_right")
        {
            CancelInvoke(nameof(StopMovementAfterDelay));
        }

        switch (command)
        {
            // ... (Logic Speed and Movement)

            case "run":
                shark.currentSpeed = shark.runSpeed;
                if (shark.voiceActive) shark.activeVoiceSpeed = shark.currentSpeed;
                break;

            case "walk":
                shark.currentSpeed = shark.walkSpeed;
                if (shark.voiceActive) shark.activeVoiceSpeed = shark.currentSpeed;
                break;
                
            case "go_up":
                ExecuteMoveCommand(Vector2.up);
                break;

            case "go_down":
                ExecuteMoveCommand(Vector2.down);
                break;

            case "go_left":
                ExecuteMoveCommand(Vector2.left);
                shark.GetComponent<SpriteRenderer>().flipX = true;
                break;

            case "go_right":
                ExecuteMoveCommand(Vector2.right);
                shark.GetComponent<SpriteRenderer>().flipX = false;
                break;

            case "turn_left":
                shark.StopMovement();
                shark.GetComponent<SpriteRenderer>().flipX = true;
                break;

            case "turn_right":
                shark.StopMovement();
                shark.GetComponent<SpriteRenderer>().flipX = false;
                break;

            case "stop":
                shark.StopMovement();
                CancelInvoke(nameof(StopMovementAfterDelay));
                break;

            case "attack": //fix like use space
                if (shooter != null)
                {
                    // call function Shoot() in PlayerShooting,
                    shooter.Shoot(); 
                    Debug.Log("VOICE CMD: Single Shot Fired (Target-Seeking)");
                }
                break;
        }
    }
}