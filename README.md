#*Stickman Jump v1.0.0*

Juego Desarrollado para la GameJam interna de Artek Institute - Sae Institute (T4 - 2023)

**Scripts:**
- GameManager: Guarda, administra los datos de la partida del jugador, asi como almnacenar los canvas de win, lose y reset.
- Audio Manager: Utilizando Arrays se mandan a llamar musica de fondo y efectos de sonido.
  - Sound: Clase que contiene el Audio y su nombre.
  - Collect: Por medio de una clasificacion de objetos se activa un efecto de sonido u otro.
- PlayerMovement: Todas las fisicas del jugador incluyendo la deteccion de suelo, paredes y la logica para controlar las acciones del juego.
- PlayerManager: Guarda la informacion temporal del jugador.
- Save System: Registra el puntaje del jugador y compara con los datos anteriormente guardados para llevar un scoreboard local.
  - PlayerInfo: Clase con la informacion del jugador para ser guardada.
- WinCondition: Encargado de Dejar o no Pasar al jugador de nivel, dependiendo de la obtencion de diferentes objetos.
- CannonLaunch: Lanza con respecto a X segundos una bola de cañon.
  - CanonBall: por medio de rigidbody añade movimiento y la funcion de Hacer X daño al jugador.
- Dialogue: Por medio de corrutinas y Metodos, se hace una escritura caracter por caracter para dar como resultado un dialogo gradual.
- UIController: Controla los sliders y las transiciones del UI.
  - UIControllerMenus: Funciones para los botones de la UI.
  - CollectionUI: Maneja los textos de la UI Principal.
- Elevator: Crea un movimiento armonico simple para mover una plataforma en 4 direcciones diferentes dependiendo de que tipo de elevador se escoje en el inspector.
- TriggerSystem: a traves de elegir un tipo de objeto se hace una accion al colisionar con el player.
  - EnemyMovement: Script encargado del movimiento de los enemigos.
  - EnemyStatic: Genera daño al jugador al colisionar con el por medio del TriggerSystem.
- PanelControl: Cuando se genera una colision del jugador con el objeto, instancia un dialogo e inicializa el texto con uno precargado.
- PlayerPlatformInteraction: La interaccion de las plataformas con el jugador.
- RotationTranslationAnimation: Genera rotacion de un jugador por medio de elegir un Eje (*Por GameDevTraum*)
