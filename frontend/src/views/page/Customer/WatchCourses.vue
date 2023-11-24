<template>
  <div class="parnet">
    <baselist v-if="this.$route.query.action != 'registed'"></baselist>
    <baseclass v-if="this.$route.query.action == 'registed'"></baseclass>
  </div>
</template>
<script>
import list from "@/components/customer/Course/List_Watch_course.vue";
import classx from "@/components/customer/Course/List_Watch_class.vue";
export default {
  components: {
    baselist: list,
    baseclass: classx,
  },
  setup() {},
};
</script>
<style scoped>
.parnet {
  position: absolute;
  inset: 0;
}
</style>
