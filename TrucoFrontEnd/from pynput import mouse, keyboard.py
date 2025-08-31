from pynput import mouse, keyboard

kb = keyboard.Controller()

def on_click(x, y, button, pressed):
    if pressed and button == mouse.Button.x2:  # botão lateral X2
        kb.press('c')

# Listener do mouse sempre ativo
with mouse.Listener(on_click=on_click) as listener:
    listener.join()