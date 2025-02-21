namespace ProjectPlanner;

public class Program()
{

    public static void Main()
    {
        ProjectPlannerContext context = new ProjectPlannerContext();
        UserService userService = new UserService(new UserRepository(context));
        Menu menu = new Menu(new ProjectService(new ProjectRepository(context)), userService);
        while (menu.running)
        {
            menu.Run();
        }
    }

}