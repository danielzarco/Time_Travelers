using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Throw_balls : MonoBehaviour
{
    public AudioSource audio;
    public ParticleSystem particle_fire;
    List<GameObject> balls;
    public GameObject Ball;
    public GameObject Camera;
    public GameObject Timer;
    public float timer_scene = 120.0f;
    public float timer_scene2 = 180.0f;
    public GameObject Button;
    public GameObject Quit_Button;
    public float timer;
    bool game_started;
    int pos;
    //public float move_delay = 0.5f;
    //bool moved;
    //float last_time_moved;
    public GameObject Left;
    public GameObject Center;
    public GameObject Right;
    public GameObject Enemy_Ball;
    List<GameObject> enemy_balls;
    public float enemy_attack = 2.0f;
    float last_enemy_attack;
    float enemy_attack_start;
    public float fire_delay = 0.25f;
    float last_fire;
    public float fire_vel = 250.0f;
    public GameObject fire_pos;
    public GameObject counter;
    [HideInInspector]
    public int count;
    int current_scene;
    public GameObject Win_Message;
    bool scene_loaded;

    // Use this for initialization
    void Start()
    {
        balls = new List<GameObject>();
        game_started = false;
        pos = 0;
        enemy_attack_start = enemy_attack;
        enemy_balls = new List<GameObject>();
        //moved = false;
        last_fire = 0.0f;
        count = 0;
        current_scene = 1;
        scene_loaded = true;

        SceneManager.sceneLoaded += loadScene2;

        DontDestroyOnLoad(this);
        DontDestroyOnLoad(Camera);
        DontDestroyOnLoad(Ball);
    }

    void loadScene2(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "Scene2")
        {
            Debug.Log("changing public variables");
            Enemy_Ball = GameObject.FindGameObjectWithTag("Enemy_Ball");
            Enemy_Ball.SetActive(false);
            Enemy_Ball.GetComponent<Enemy_Ball_Script>().Manager = this.gameObject;
            GameObject[] tmp = GameObject.FindGameObjectsWithTag("Enemy_Canons");
            Debug.Log(tmp.Length.ToString());
            Right = tmp[0];
            Center = tmp[1];
            Left = tmp[2];
            scene_loaded = true;
        }
    }

    public void StartGame()
    {
        if (scene_loaded)
        {
            game_started = true;
            CleanUp();
            Button.SetActive(false);
            Quit_Button.SetActive(false);
            timer = 0.0f;
            //last_time_moved = 0.0f;
            last_enemy_attack = timer;
            last_fire = 0.0f;
            count = 0;
            if (current_scene == 1)
            {
                enemy_attack = enemy_attack_start;
            }
            else if (current_scene == 2)
            {
                enemy_attack = enemy_attack_start * 0.75f;
            }
        }
    }

    void CleanUp()
    {
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        balls.Clear();

        foreach (GameObject ball in enemy_balls)
        {
            Destroy(ball);
        }
        enemy_balls.Clear();
        /*if (current_scene != 1)
        {
            current_scene = 1;
            SceneManager.LoadScene("Scene");
        }*/
    }

    public void Delete_Ball(GameObject ball)
    {
        bool find = false;
        foreach (GameObject tmp_ball in balls)
        {
            if (tmp_ball == ball)
            {
                find = true;
                break;
            }
        }
        if (find)
        {
            balls.Remove(ball);
            Destroy(ball);
            return;
        }

        foreach (GameObject tmp_ball in enemy_balls)
        {
            if(tmp_ball == ball)
            {
                find = true;
                break;
            }
        }
        if (find)
        {
            enemy_balls.Remove(ball);
            Destroy(ball);
        }
    }

    public void GameOver()
    {
        CleanUp();
        game_started = false;
        Button.SetActive(true);
        Quit_Button.SetActive(true);
        if (pos == 1)
        {
            Camera.transform.Translate(Vector3.right * -1.25f);
            pos--;
        }
        else if(pos == -1)
        {
            Camera.transform.Translate(Vector3.right * 1.25f);
            pos++;
        }
        //moved = false;
    }

    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    void WinScene1()
    {
        Debug.Log("loading scene2");
        SceneManager.LoadScene("Scene2");
        current_scene = 2;
        Button.SetActive(true);
        Quit_Button.SetActive(true);
        game_started = false;
        scene_loaded = false;
    }

    void WinScene2()
    {
        game_started = false;
        Win_Message.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (game_started)
        {
            timer += Time.deltaTime;

            /*if (!moved)
            {
                if (pos > -1 && Input.GetAxis("Mouse X") < -2.0f)//left
                {
                    Camera.transform.Translate(Vector3.right * -1.25f);
                    pos--;
                    Debug.Log("Left");
                    moved = true;
                    last_time_moved = 0.0f;
                }

                if (pos < 1 && Input.GetAxis("Mouse X") > 2.0f)//right
                {
                    Camera.transform.Translate(Vector3.right * 1.25f);
                    pos++;
                    Debug.Log("Right");
                    moved = true;
                    last_time_moved = 0.0f;
                }
            }
            else
            {
                last_time_moved += Time.deltaTime;
                if(last_time_moved >= move_delay)
                    moved = false;
            }
            if (pos > -1 && Input.GetAxis("Mouse ScrollWheel") < 0.0f)//left
            {
                Camera.transform.Translate(Vector3.right * -1.25f);
                pos--;
                Debug.Log("Left");
            }

            if (pos < 1 && Input.GetAxis("Mouse ScrollWheel") > 0.0f)//right
            {
                Camera.transform.Translate(Vector3.right * 1.25f);
                pos++;
                Debug.Log("Right");
            }*/

            if (timer > (last_enemy_attack + enemy_attack))
            {
                int r = Random.Range(0, 3);

                GameObject tmp = Instantiate(Enemy_Ball);
                tmp.SetActive(true);
                switch (r)
                {
                    case 0:
                        tmp.GetComponent<Transform>().transform.position = Left.transform.position;
                        tmp.GetComponent<Rigidbody>().velocity = Left.transform.forward * (-10 - (timer / 10));
                        break;

                    case 1:
                        tmp.GetComponent<Transform>().transform.position = Center.transform.position;
                        tmp.GetComponent<Rigidbody>().velocity = Center.transform.forward * (-10 - (timer / 10));
                        break;

                    case 2:
                        tmp.GetComponent<Transform>().transform.position = Right.transform.position;
                        tmp.GetComponent<Rigidbody>().velocity = Right.transform.forward * (-10 - (timer / 10));
                        break;

                    default:
                        break;
                }
                enemy_balls.Add(tmp);
                Debug.Log("enemy ball created");

                last_enemy_attack = timer;
                enemy_attack -= (enemy_attack * 0.0125f);
            }

            if ((timer - last_fire) > fire_delay)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    GameObject tmp_b = Instantiate(Ball);
                    tmp_b.SetActive(true);
                    tmp_b.GetComponent<Rigidbody>().velocity = Camera.transform.forward * fire_vel;
                    tmp_b.GetComponent<Transform>().transform.position = fire_pos.transform.position;
                    balls.Add(tmp_b);
                    audio.Play();
                    particle_fire.Play();
                    Debug.Log("ball created");

                    last_fire = timer;
                }
            }

            if (current_scene == 1 && timer >= timer_scene)
            {
                WinScene1();
            }
            if(current_scene == 2 && timer >= timer_scene2)
            {
                WinScene2();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject tmp_b = Instantiate(Ball);
                tmp_b.SetActive(true);
                tmp_b.GetComponent<Rigidbody>().velocity = Camera.transform.forward * fire_vel;
                tmp_b.GetComponent<Transform>().transform.position = fire_pos.transform.position;
                balls.Add(tmp_b);
                audio.Play();
                particle_fire.Play();
                Debug.Log("ball created");
            }
        }

        Timer.GetComponent<Text>().text = System.Math.Round(timer, 3).ToString();
        if(counter)
        {
            counter.GetComponent<Text>().text = count.ToString();
        }

        if (balls.Count > 25)
        {
            GameObject tmp = balls[0];
            balls.Remove(tmp);
            Destroy(tmp);
        }
    }
}
