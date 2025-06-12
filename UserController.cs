using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]  // Base route: /api/users
public class UserController : ControllerBase
{
    private static List<User> users = new List<User>();

    // 🔹 GET: Retrieve all users
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        try
        {
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    // 🔹 GET: Retrieve a user by ID
    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        return user != null
            ? Ok(user)
            : NotFound($"User with ID {id} not found.");
    }


    // 🔹 POST: Create a new user
    [HttpPost]
    public IActionResult CreateUser(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email))
        return BadRequest("Name and Email are required.");

        if (!user.Email.Contains("@"))
        return BadRequest("Invalid email format.");

        users.Add(user);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    // 🔹 PUT: Update user details
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, User updatedUser)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();
        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;
        return Ok(user);
    }

    // 🔹 DELETE: Remove a user
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();
        users.Remove(user);
        return NoContent();
    }
}