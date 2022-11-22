using Unity.Mathematics;
using UnityEngine;

namespace CosmoSimClone
{

    [RequireComponent(typeof(SpaceShip))]

    public class AIController : MonoBehaviour
    {
        public enum AIBehavior
        {
            Null,
            Patrol,
            RoutePatrol
        }
        /// <summary>
        /// Тип поведения бота
        /// </summary>
        [SerializeField] private AIBehavior m_AIBehavior;

        [SerializeField] private AIPointPatrol m_PatrolPoint;

        [Range(0f, 1f)]
        [SerializeField] private float m_NavigationLinear;

        [Range(0f, 1f)]
        [SerializeField] private float m_NavigationAngular;

        [SerializeField] private float m_RandomSelectedMovePointTime;

        [SerializeField] private float m_FindNewTargetTime;

        [SerializeField] private float m_ShootDelay;

        [SerializeField] private float m_EvadeRayLength;

        [SerializeField] private float stepLength;

        [SerializeField] private Transform[] m_PointsOfRoutePatrol;

        private SpaceShip m_SpaceShip;

        private Vector3 m_MovePosition;

        private Vector3 m_LeadPointForMovePosition;

        private Destructible m_SelectedTarget;

        private Timer m_RandomizeDirectionTimer;
        private Timer m_FireTimer;
        private Timer m_FindNewTargetTimer;

        private int patrolPointIndex = 0;

        private int m_currentTeamId;

        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>();
            InitTimers();
            m_currentTeamId = m_SpaceShip.TeamId;
            m_currentThrust = m_NavigationLinear;
            IsEvading = false;
        }

        #region Timers

        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectedMovePointTime);
            m_FireTimer = new Timer(m_ShootDelay);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
            m_EvadeTimer = new Timer(m_EvadeTime);
        }

        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
            m_EvadeTimer.RemoveTime(Time.deltaTime);
        }

        #endregion

        private void Update()
        {
            UpdateTimers();

            UpdateAI();
        }

        private void UpdateAI()
        {
            if (m_AIBehavior == AIBehavior.Null)
            {

            }

            if (m_AIBehavior == AIBehavior.Patrol)
            {
                UpdateBehaviourPatrol();
            }

            if (m_AIBehavior == AIBehavior.RoutePatrol)
            {
                UpdateBehaviourPatrol();
            }
        }

        private void UpdateBehaviourPatrol()
        {
            ActionFindNewMovePosition();
            ActionEvadeCollision();
            ActionControlShip();
            ActionFire();
            
        }
        private void ActionFindNewMovePosition()
        {
            if(m_AIBehavior == AIBehavior.Patrol)
            {
                if(m_SelectedTarget != null)
                {
                    if (IsEvading == false)
                    {
                        float dist = Vector2.Distance(transform.position, m_SelectedTarget.transform.position);
                        m_LeadPointForMovePosition = new Vector3(0, 0.3f, 0);
                        m_MovePosition = m_SelectedTarget.transform.position + m_LeadPointForMovePosition;
                        if (dist < 3f)
                        {
                            m_NavigationLinear = 0;
                        }
                        else
                        {
                            m_NavigationLinear = m_currentThrust;
                        }
                    }
                }
                else
                {
                    if (m_PatrolPoint != null && IsEvading == false)
                    {
                        bool IsInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius;
                        m_NavigationLinear = m_currentThrust;
                        if (IsInsidePatrolZone == true)
                        {
                            if(m_RandomizeDirectionTimer.IsFinished == true)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;

                                m_MovePosition = newPoint;

                                m_RandomizeDirectionTimer.Start(m_RandomSelectedMovePointTime);
                            }
                        }
                        else
                        {
                            m_MovePosition = m_PatrolPoint.transform.position;
                        }
                    }
                }
            }

            if(m_AIBehavior == AIBehavior.RoutePatrol)
            {
                if (m_SelectedTarget != null)
                {
                    if (IsEvading == false)
                    {
                        float dist = Vector2.Distance(transform.position, m_SelectedTarget.transform.position);
                        m_LeadPointForMovePosition = new Vector3(0, 0.3f, 0);
                        m_MovePosition = m_SelectedTarget.transform.position + m_LeadPointForMovePosition;
                        if (dist < 4f)
                        {
                            m_NavigationLinear = 0;
                        }
                        else
                        {
                            m_NavigationLinear = m_currentThrust;
                        }
                    }
                }
                else
                {
                    if (m_PointsOfRoutePatrol != null && IsEvading == false)
                    {
                        m_MovePosition = m_PointsOfRoutePatrol[patrolPointIndex].transform.position;
                        m_NavigationLinear = m_currentThrust;
                        float dist = Vector2.Distance(m_SpaceShip.transform.position, m_PointsOfRoutePatrol[patrolPointIndex].transform.position);
                        if (dist < 3f)
                        {
                            patrolPointIndex++;
                        }
                    }
                    if (patrolPointIndex > m_PointsOfRoutePatrol.Length - 1)
                    {
                        patrolPointIndex = 0;
                    }

                }

                
            }
        }
        private float m_currentThrust;
        private Timer m_EvadeTimer;
        private float m_EvadeTime = 10f;
        private bool IsEvading;
        private void ActionEvadeCollision()
        {
            if (m_EvadeTimer.IsFinished == true)
            {
                IsEvading = false;
            } 
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength);

            if (hit)
            {
                
                Destructible dest = hit.collider.transform.GetComponentInParent<Destructible>();

                

                

                if (dest != null)
                {
                    float dist = Vector2.Distance(transform.position, dest.transform.position);
                    if (dest.TeamId == m_SpaceShip.TeamId || dest.TeamId == Destructible.TeamIdNeutral)
                    {
                        
                        m_MovePosition = transform.position + transform.right * 3f;
                        if (dist < 2f)
                        {
                            IsEvading = true;
                            m_EvadeTimer.Start(m_EvadeTime);
                            m_NavigationLinear = 0;
                            m_SpaceShip.TorqueControl = 0.5f;
                            Debug.Log("ShipStopped!");
                        }
                        if (IsEvading == false)
                        {
                            m_SpaceShip.TorqueControl = ComputeAlignTargetNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
                            m_NavigationLinear = m_currentThrust;
                            Debug.Log("ShipIsRunning");
                        }
                    }
                }
            }
        }

        private void ActionControlShip()
        {
                m_SpaceShip.ThrustControl = m_NavigationLinear;
                m_SpaceShip.TorqueControl = ComputeAlignTargetNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }

        private const float MAX_ANGLE = 45.0f;

        private static float ComputeAlignTargetNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }
        private void ActionFindNewAttackTarget()
        {
            if(m_FindNewTargetTimer != null)
            {
                if (m_FindNewTargetTimer.IsFinished == true)
                {
                    m_SelectedTarget = FindNearestDestructibleTarget();

                    m_FindNewTargetTimer.Start(m_FindNewTargetTime);
                }
            }
        }
        private void ActionFire()
        {
            if (m_SelectedTarget != null)
            {
                if (m_FireTimer.IsFinished == true)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

                    if (hit)
                    {
                        Destructible dest = hit.collider.transform.GetComponentInParent<Destructible>();

                        if(dest != null && dest.TeamId != m_SpaceShip.TeamId && dest.TeamId != Destructible.TeamIdNeutral)
                        {
                            m_SpaceShip.Fire(TurretMode.Main);

                            m_FireTimer.Start(m_ShootDelay);  //to do restart timer
                        }
                    }
                    else
                    {

                    }
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            Destructible dest = collision.transform.GetComponentInParent<Destructible>();
            DamageZone zone = collision.transform.GetComponent<DamageZone>();
            PowerUp powerup = collision.transform.GetComponent<PowerUp>();
            if (zone != null) return;
        
            if (powerup != null) return;
        
            if (dest != null)
            {
                if (m_SelectedTarget != null) return;
                if (dest.TeamId == m_currentTeamId || dest.TeamId == Destructible.TeamIdNeutral) return;
                if (dest.TeamId != m_currentTeamId)
                {
                    ActionFindNewAttackTarget();
                }
            }
        }

        private Destructible FindNearestDestructibleTarget()
        {
            float maxDist = float.MaxValue;

            Destructible potentialTarget = null;

            foreach(var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == m_SpaceShip) continue;

                if (v.TeamId == Destructible.TeamIdNeutral) continue;

                if (v.TeamId == m_SpaceShip.TeamId) continue;

                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }

            return potentialTarget;
        }

        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehavior = AIBehavior.Patrol;
            m_PatrolPoint = point;
        }

    }
}