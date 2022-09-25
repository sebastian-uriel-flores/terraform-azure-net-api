output "resource_name_suffix" {
  description = "Suffix of the name of every resource"
  value       = random_string.demo.result
}