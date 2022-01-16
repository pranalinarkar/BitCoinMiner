module Config

open Akka
open Akka.FSharp

let seedAkkaConfig hostName port = Configuration.parse("""
akka {  
	actor {
		provider = "Akka.Cluster.ClusterActorRefProvider, Akka.Cluster"
		serializers {
			json = "Akka.Serialization.HyperionSerializer, Akka.Serialization.Hyperion"
		}
		serialization-bindings {
			"System.Object" = json
		}
		deployment {
			/minerProvider = {
				router = round-robin-pool
				metrics-selector = cpu
				nr-of-instances = 40
				cluster {
					enabled = on
					use-role = worker
					allow-local-routees = on
					max-nr-of-instances-per-node = 10
				}
			}
			/generatorProvider = {
				router = broadcast-pool
				metrics-selector = cpu
				nr-of-instances = 20
				cluster {
					enabled = on
					use-role = worker
					allow-local-routees = on
					max-nr-of-instances-per-node = 5
				}
			}
		}
	}
	remote {
		log-remote-lifecycle-events = off
		helios.tcp {
			hostname = """ + hostName + """
			port = """ + port + """       
		}
	}
	cluster {
		min-nr-of-members = 1
		roles = [master, worker]
		role {
			master.min-nr-of-members = 1
			worker.min-nr-of-members = 1
		}
		seed-nodes = ["akka.tcp://coin-mining-cluster@""" + hostName + """:""" + port + """"]
		# when node cannot be reached within 10 sec, mark is as down
		auto-down-unreachable-after = 20 s
		failure-detector {
			heartbeat-interval = 1 s
			threshold = 8.0
			max-sample-size = 1000
			min-std-deviation = 100 ms
			acceptable-heartbeat-pause = 20 s
			expected-response-after = 1 s
		}
	}
	log-dead-letters = 0
	log-dead-letters-during-shutdown = off
}
""")

let clientAkkaConfig hostName port seedHostName = Configuration.parse("""
akka {  
	actor {
		provider = "Akka.Cluster.ClusterActorRefProvider, Akka.Cluster"
		serializers {
			json = "Akka.Serialization.HyperionSerializer, Akka.Serialization.Hyperion"
		}
		serialization-bindings {
			"System.Object" = json
		}
	}
	remote {
		log-remote-lifecycle-events = off
		helios.tcp {
			hostname = """ + hostName + """
			port = 0       
		}
	}
	cluster {
		roles = ["worker"]  # custom node roles
		seed-nodes = ["akka.tcp://coin-mining-cluster@""" + seedHostName + """:""" + port + """"]
		# when node cannot be reached within 10 sec, mark is as down
		auto-down-unreachable-after = 20 s
		failure-detector {
			heartbeat-interval = 1 s
			threshold = 8.0
			max-sample-size = 1000
			min-std-deviation = 100 ms
			acceptable-heartbeat-pause = 20 s
			expected-response-after = 1 s
		}
	}
	log-dead-letters = 0
	log-dead-letters-during-shutdown = off
}
""")

let singleNodeConfig = Configuration.parse("""
akka {
    actor {
        deployment {
            /minerProvider {
                router = round-robin-pool
                nr-of-instances = 50
            }
            /generatorProvider{
                router = broadcast-pool
                nr-of-instances = 5
            }
        }
    }
}
""")
