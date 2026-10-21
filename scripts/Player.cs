using Godot;

public partial class Player : CharacterBody2D
{
	private const float Speed = 30.0f;
	private const float JumpVelocity = -400.0f;
	[Export] private AnimatedSprite2D _anim;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}
		
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}
		
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
		
		if (direction.X > 0f)
			_anim.FlipH = false;
		else if(direction.X < 0f)
			_anim.FlipH = true;

		Velocity = velocity;
		MoveAndSlide();
	}
}
