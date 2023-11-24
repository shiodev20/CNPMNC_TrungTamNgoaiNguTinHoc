<template>
  <div class="exam_parent">
    <baselist v-if="this.$route.query.action == null"></baselist>
    <baseadd
      v-if="
        this.$route.query.action == 'create' ||
        this.$route.query.action == 'edit'
      "
    ></baseadd>
  </div>
</template>
<script>
import add from "@/components/languages/Add_Languages.vue";
import list from "@/components/languages/List_Languages.vue";
export default {
  name: "ExamV",
  components: {
    baseadd: add,
    baselist: list,
  },
};
</script>
<style scoped></style>
