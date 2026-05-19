using System;

public class ApotekRepository
{
	private MedicinContext _context;

	public ApotekRepository(MedicinContext medicinContext)
	{
		_context = medicinContext;
	}

}
