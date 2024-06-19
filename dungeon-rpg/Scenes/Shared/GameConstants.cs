using Godot;

public class GameConstants
{
    public const string ANIM_IDLE = "idle";
    public const string ANIM_MOVE = "move";
    public const string ANIM_DASH = "dash";
    public const string ANIM_ATTACK = "attack";
    public const string ANIM_ATTACK_KICK = "attack_kick";
    public const string ANIM_ATTACK_SLASH = "attack_slash";

    public const string INPUT_MOVE_LEFT = "move_left";
    public const string INPUT_MOVE_RIGHT = "move_right";
    public const string INPUT_MOVE_FORWARD = "move_forward";
    public const string INPUT_MOVE_BACKWARD = "move_backward";
    public const string INPUT_DASH = "dash";
    public const string INPUT_ATTACK = "attack";

    public const int NOTIFICATION_ENTER_STATE = 5001;
    public const int NOTIFICATION_EXIT_STATE = 5002;
}