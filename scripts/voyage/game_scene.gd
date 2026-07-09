extends Control

const VOYAGE_SECONDS := 300.0
const SPEED_NAMES: Array[String] = ["느림", "보통", "빠름"]
const LETTER_TEXTS: Array[String] = [
	"오늘은 아무것도 증명하지 않아도 괜찮아요.",
	"조용한 바다는 당신의 속도를 기다려줍니다.",
	"작은 항해도 충분히 멀리 갈 수 있어요."
]
const SCENERY_NAMES: Array[String] = ["일출", "비", "고래", "밤바다"]

var remaining_seconds := VOYAGE_SECONDS
var speed_index := 1
var appreciation_mode := false
var voyage_record_created := false


func _ready() -> void:
	randomize()
	%TakePhotoButton.pressed.connect(_take_photo)
	%AppreciationButton.pressed.connect(_toggle_appreciation_mode)
	%SpeedButton.pressed.connect(_cycle_speed)
	%LetterButton.pressed.connect(_find_letter)
	%SceneryButton.pressed.connect(_record_scenery)
	%AlbumButton.pressed.connect(_open_album)
	_update_ui("동반자가 곁에서 조용히 바다를 바라봅니다.")


func _process(delta: float) -> void:
	if remaining_seconds <= 0.0:
		return

	remaining_seconds = maxf(0.0, remaining_seconds - delta)
	if remaining_seconds <= 0.0 and not voyage_record_created:
		voyage_record_created = true
		_update_ui("오늘의 항해 기록이 만들어졌습니다. 이제 편히 머물러도 좋아요.")
	else:
		_update_ui()


## Adds a simple photo record to the album.
func _take_photo() -> void:
	GameState.add_photo("사진 %d - %s의 바다" % [GameState.photos.size() + 1, GameState.selected_mood])
	_update_ui("사진을 한 장 남겼습니다. 동반자가 가까이 다가옵니다.")


## Toggles quiet appreciation mode.
func _toggle_appreciation_mode() -> void:
	appreciation_mode = not appreciation_mode
	var message := "감상모드로 전환했습니다." if appreciation_mode else "기본 화면으로 돌아왔습니다."
	_update_ui(message)


## Cycles drift speed between slow, normal, and fast.
func _cycle_speed() -> void:
	speed_index = (speed_index + 1) % SPEED_NAMES.size()
	_update_ui("표류 속도를 %s으로 바꿨습니다." % SPEED_NAMES[speed_index])


## Shows one local bottle letter.
func _find_letter() -> void:
	var letter := LETTER_TEXTS[randi() % LETTER_TEXTS.size()]
	GameState.add_letter(letter)
	_update_ui("병 속 편지를 발견했습니다: %s" % letter)


## Records one scenery event.
func _record_scenery() -> void:
	var scenery := SCENERY_NAMES[randi() % SCENERY_NAMES.size()]
	GameState.add_scenery(scenery)
	_update_ui("%s 풍경을 앨범에 기록했습니다." % scenery)


func _open_album() -> void:
	get_tree().change_scene_to_file("res://scenes/album.tscn")


func _update_ui(message: String = "") -> void:
	%MoodStatusLabel.text = "마음: %s / 동반자 Lv %d" % [GameState.selected_mood, GameState.companion_affection]
	%TimerLabel.text = _format_time(remaining_seconds)
	%SpeedButton.text = "속도: %s" % SPEED_NAMES[speed_index]
	if message != "":
		%StatusLabel.text = message


func _format_time(seconds: float) -> String:
	var total_seconds := int(ceil(seconds))
	var minutes := int(total_seconds / 60)
	var seconds_left := total_seconds % 60
	return "%02d:%02d" % [minutes, seconds_left]
