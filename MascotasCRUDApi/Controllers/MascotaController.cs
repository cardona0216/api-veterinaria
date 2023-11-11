using AutoMapper;
using MascotasCRUDApi.Models;
using MascotasCRUDApi.Models.DTO;
using MascotasCRUDApi.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MascotasCRUDApi.Controllers
{
    //los conttoladores los usaremos para controlar las peticiones desde el frontend 
    //hacie el backend y backend es decir este sera el que controlara las peticines
    //vamos a crear 5 metodos
    //1 get
    //2 get/{id}
    //3 post
    //4 put
    //5 delete
    //es decir el crud

    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        //para poder acceder al contexto de debemos de acceder al contexto,
        // que es que intectua con la base de datos Models/AplicationDbContext.cs


        //con esto estamos declarando una variable de solo lectura
        //esto lo hacemos para hacer la inyeccion de dependencias
        // como anteriormente ya se declaro en el program.cs 
        // using MascotasCRUDApi.Models; con esto podemos usar nuetro contexto de 
        // manera global en todo aquello lo necesite(clase-controlador algun servico)
        //podra usar el contexto de la aplicacion atravez de inyeccion de dependencias
       
        private readonly IMapper _mapper;
        private IMascotaRepository _mascotaRepository;

        //creamos el constructor de esta clase

        public MascotaController( IMapper mapper, IMascotaRepository mascotaRepository)
        {
            
            _mapper = mapper;
             _mascotaRepository = mascotaRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                
                var listMascotas = await _mascotaRepository.GetListMascotas();
                //usamos el DTO
                var listMascotasDTO = _mapper.Map<IEnumerable<MascotaDTO>>(listMascotas); 
                return Ok(listMascotasDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var mascota = await _mascotaRepository.GetMascota(id);
                if (mascota == null)
                {
                    return NotFound();
                }
                var mascotaDTO = _mapper.Map<MascotaDTO>(mascota);
                return Ok(mascotaDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //borrar
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var mascota = await _mascotaRepository.GetMascota(id);
                if (mascota == null)
                {
                    return NotFound();
                }
                await _mascotaRepository.DeleteMascota(mascota);
                return NoContent();
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Post(MascotaDTO mascotaDto)
        {

            try
            {
                var mascota = _mapper.Map<Mascota>(mascotaDto);

                mascota.FechaCreacion = DateTime.Now;

                mascota = await _mascotaRepository.AddMascota(mascota);

                var mascotaItemDto = _mapper.Map<MascotaDTO>(mascota);
                return CreatedAtAction("Get", new { id = mascotaItemDto.Id }, mascotaItemDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MascotaDTO mascotaDto)
        {
            try
            {
                var mascota = _mapper.Map<Mascota>(mascotaDto);
                if (id != mascota.Id)
                {
                    return BadRequest();
                }

                //_context.Update(mascota);// con esto se editan todos los valores
                var mascotaItem = await _mascotaRepository.GetMascota(id);

                if (mascotaItem == null)
                {
                    return NotFound();
                }

                await _mascotaRepository.UpdateMascota(mascota);

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
