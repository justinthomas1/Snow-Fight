public class map_settings{
	private static bool isTimeAttack;

    public static bool TimeAttack{
        get{
            return isTimeAttack;
        }
        set{
            isTimeAttack = value;
        }
    }
}
